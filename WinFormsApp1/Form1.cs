using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumUndetectedChromeDriver;
using System.Diagnostics;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private ChromeDriver driver;
        private bool isRunning = false;
        private System.Windows.Forms.Timer timerCountdown; // Timer đếm ngược
        private int countdownTime; // Thời gian đếm ngược (tính bằng giây)
        public Form1()
        {

            InitializeComponent();
            InitializeTimer();
        }
        private void InitializeTimer()
        {
            timerCountdown = new System.Windows.Forms.Timer();
            timerCountdown.Interval = 1000; // Mỗi giây
            timerCountdown.Tick += TimerCountdown_Tick;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label_content_Click(object sender, EventArgs e)
        {

        }

        private void button_attack_Click(object sender, EventArgs e)
        {
            isRunning = true;
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            button_attack.Enabled = false;
            button_stop.Enabled = true;
            Thread thread = new Thread(Attack);
            thread.IsBackground = true;  
            thread.Start();
        }
        public void OpenChromeWithProfile(string userDataDir, string profileName)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument($"--user-data-dir={userDataDir}");
            options.AddArgument($"--profile-directory={profileName}");

            // Khởi tạo WebDriver với profile
            IWebDriver driver = new ChromeDriver(options);

            // Điều hướng tới một URL để kiểm tra (có thể thay đổi URL)
            driver.Navigate().GoToUrl(textBox_website.Text);

            // Đóng trình duyệt sau khi hoàn tất (hoặc giữ mở nếu cần)
            // Để đóng trình duyệt sau thời gian chờ nhất định:
            Thread.Sleep(10000); // Đợi 10 giây
            driver.Quit();
        }

        static List<string> GetAllProfiles(string userDataDir)
        {
            var profiles = new List<string>();
            // Lấy tất cả các thư mục trong User Data
            string[] directories = Directory.GetDirectories(userDataDir);

            foreach (string dir in directories)
            {
                string folderName = Path.GetFileName(dir);

                if (
                    folderName.StartsWith("Profile "))
                {
                    profiles.Add(folderName);
                }
            }

            return profiles;
        }
        private async void Attack()
        {
            while (isRunning)
            {
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string userDataDir = Path.Combine(userProfile, "AppData", "Local", "Google", "Chrome", "User Data");
                string url = textBox_website.Text;
                var profiles = GetAllProfiles(userDataDir);

                // Thực hiện click tự động cho tất cả các profile
                foreach (string profile in profiles)
                {
                    string scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "index.js");

                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "node",
                        Arguments = $"\"{scriptPath}\" \"{userDataDir}\" \"{profile}\" \"{url}\"",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };
                    try
                    {
                        Process process = Process.Start(startInfo);
                        process.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Có lỗi khi thực thi mã: {ex.Message}");
                    }
                }

                int minutes = int.Parse(numeric_after.Text);  
                int delayInMilliseconds = minutes * 60 * 1000;
                countdownTime = minutes * 60; 

                Invoke((MethodInvoker)(() =>
                {
                    lblCountdown.Text = TimeSpan.FromSeconds(countdownTime).ToString(@"mm\:ss");
                    lblCountdown.Visible = true;
                }));

                timerCountdown.Start();

                // Chờ đếm ngược hoàn tất
                while (countdownTime > 0)
                {
                    await Task.Delay(100); 
                }

                Invoke((MethodInvoker)(() =>
                {
                    lblCountdown.Visible = false;
                }));

                // Chờ thêm thời gian trì hoãn nếu cần
                await Task.Delay(delayInMilliseconds);

                // Ẩn progress bar (nếu cần)
                Invoke((MethodInvoker)(() =>
                {
                    progressBar.Visible = false;
                }));
            }
        }

        private void TimerCountdown_Tick(object sender, EventArgs e)
        {
            // Debug MessageBox để kiểm tra countdown
            MessageBox.Show($"Countdown: {countdownTime}");
            if (countdownTime > 0)
            {
                countdownTime--; // Giảm thời gian mỗi giây
                // Cập nhật lại lblCountdown từ UI thread
                Invoke((MethodInvoker)(() =>
                {
                    lblCountdown.Text = TimeSpan.FromSeconds(countdownTime).ToString(@"mm\:ss");
                }));
            }
            else
            {
                timerCountdown.Stop(); // Dừng Timer nếu thời gian = 0
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            isRunning = false;  
            progressBar.Visible = false;
            button_attack.Enabled = true;
            MessageBox.Show("Tiến trình đã dừng!");
        }

        private void button_selectfile_Click(object sender, EventArgs e)
        {
            
        }
    }
}
