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

        public Form1()
        {

            InitializeComponent();


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
            //Thread thread = new Thread(Attack);
            //thread.Start();
            progressBar.Visible = true;  
            progressBar.Style = ProgressBarStyle.Marquee; 
            button_attack.Enabled = false;
            button_stop.Enabled = true;
            // Khởi tạo thread để thực hiện Attack
            Thread thread = new Thread(Attack);
            thread.IsBackground = true;  // Đảm bảo thread là background để không chặn việc đóng ứng dụng
            thread.Start();
            MessageBox.Show("Tiến trình bắt đầu!");
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
                string userDataDir = @$"{textBox_chrome.Text}";
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

                // Lấy thời gian trì hoãn từ TextBox
                int minutes = int.Parse(numeric_after.Text);  
                int delayInMilliseconds = minutes * 60 * 1000; 

                await Task.Delay(delayInMilliseconds);

                Invoke((MethodInvoker)(() =>
                {
                    progressBar.Visible = false; 
                }));
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
