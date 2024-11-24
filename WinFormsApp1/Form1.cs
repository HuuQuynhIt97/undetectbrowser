using Microsoft.Win32;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumUndetectedChromeDriver;
using System.Diagnostics;
using System.Security.Policy;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private ChromeDriver driver;
        private bool isRunning = false;
        private System.Windows.Forms.Timer timerCountdown; // Timer đếm ngược
        private int countdownTime; // Thời gian đếm ngược (tính bằng giây)

        private System.Windows.Forms.Timer countdownTimer; // Timer để đếm ngược
        private int remainingTimeInSeconds; // Thời gian còn lại (tính bằng giây)
        public Form1()
        {
            InitializeComponent();
            numeric_after.Text = LoadSettings().ToString();
            InitializeTimer();
        }
        private void InitializeTimer()
        {
            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000; 
            countdownTimer.Tick += CountdownTimer_Tick; 
        }
        // Sự kiện khi Timer chạy
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (remainingTimeInSeconds > 0)
            {
                remainingTimeInSeconds--; // Giảm thời gian
                UpdateTimeDisplay(); // Cập nhật giao diện hiển thị
            }
            else
            {
                countdownTimer.Stop(); // Dừng Timer khi hết thời gian
                MessageBox.Show("Hết thời gian!");
            }
        }
        private void UpdateTimeDisplay()
        {
            int minutes = remainingTimeInSeconds / 60;
            int seconds = remainingTimeInSeconds % 60;
            lblCountdown.Text = $"{minutes:D2}:{seconds:D2}"; // Hiển thị dạng MM:SS
            lblCountdown.Font = new Font("Arial", 12, FontStyle.Bold); // Font lớn và đậm
            lblCountdown.TextAlign = ContentAlignment.MiddleCenter;  // Căn giữa nội dung
            lblCountdown.ForeColor = Color.DarkGreen; // Màu chữ (xanh đậm)
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label_content_Click(object sender, EventArgs e)
        {

        }

        private void button_attack_Click(object sender, EventArgs e)
        {
            if (int.TryParse(numeric_after.Text, out int minutes))
            {
                lblCountdown.Visible = true;
                remainingTimeInSeconds = minutes * 60;
                UpdateTimeDisplay();
                countdownTimer.Start(); 
            }
            isRunning = true;
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            button_attack.Enabled = false;
            button_stop.Enabled = true;
            int timeInterval = int.Parse(numeric_after.Text);
            SaveSettings(timeInterval);
            Thread thread = new Thread(Attack);
            thread.IsBackground = true;  
            thread.Start();
        }
        private int LoadSettings()
        {
            if (File.Exists("settings.json"))
            {
                var settings = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("settings.json"));
                return (int)settings.IntervalTime;
            }
            return 60; // Giá trị mặc định nếu không tìm thấy file
        }
        private void SaveSettings(int intervalTime)
        {
            var settings = new { IntervalTime = intervalTime };
            File.WriteAllText("settings.json", JsonConvert.SerializeObject(settings));
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

                remainingTimeInSeconds = minutes * 60; 
                UpdateTimeDisplay();
                countdownTimer.Start(); 

                //countdownTime = minutes * 60; 

                //Invoke((MethodInvoker)(() =>
                //{
                //    lblCountdown.Text = TimeSpan.FromSeconds(countdownTime).ToString(@"mm\:ss");
                //    lblCountdown.Visible = true;
                //}));

                //timerCountdown.Start();

                //// Chờ đếm ngược hoàn tất
                //while (countdownTime > 0)
                //{
                //    await Task.Delay(100); 
                //}

                //Invoke((MethodInvoker)(() =>
                //{
                //    lblCountdown.Visible = false;
                //}));

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
            countdownTimer.Stop(); // Dừng Timer
            lblCountdown.Visible = false;
            progressBar.Visible = false;
            button_attack.Enabled = true;
            MessageBox.Show("Tiến trình đã dừng!");
        }

        private void button_selectfile_Click(object sender, EventArgs e)
        {
            
        }
    }
}
