from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from seleniumbase import BaseCase
import time
BaseCase.main(__name__, __file__, "--uc", "-s")
# Đường dẫn User Data và Profile
user_data_path = r"C:\Users\Administrator\AppData\Local\Google\Chrome\User Data"
profile_name = "Profile 2"

# Thiết lập Chrome Options
chrome_options = Options()
chrome_options.add_argument(f"--user-data-dir={user_data_path}")
chrome_options.add_argument(f"--profile-directory={profile_name}")

# Khởi tạo WebDriver của Selenium
driver = webdriver.Chrome(options=chrome_options)

# Sử dụng SeleniumBase
class CaptchaSolver(BaseCase):
    def solve_captcha(self):
        # Mở trang web
        url = "https://freebitco.in/"
        driver.get(url)
        # Đợi trang tải
        time.sleep(5)
        print("Trang đã tải, đang thực hiện giải CAPTCHA...")
        # Sử dụng hàm của SeleniumBase để giải CAPTCHA
        self.uc_open_with_reconnect(driver, url)
        self.uc_gui_click_captcha(driver)

        # Đợi một chút sau khi giải CAPTCHA
        time.sleep(10)

        # Đóng trình duyệt
        driver.quit()

if __name__ == "__main__":
    solver = CaptchaSolver()
    solver.solve_captcha()