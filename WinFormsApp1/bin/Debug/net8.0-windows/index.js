
const test = require('node:test');
const assert = require('node:assert');
const { connect } = require('puppeteer-real-browser');
const userDataDir = process.argv[2]; 
const profileName = process.argv[3];
const url = process.argv[4];
// const url = 'https://freebitco.in/';
// const userDataDir = 'C:/Users/Administrator/AppData/Local/Google/Chrome/User Data'; 
// const profileName = 'Profile 3';
test('Puppeteer Extra Plugin', async () => {
    const { page, browser } = await connect({
        args: [
            "--start-maximized",
            `--user-data-dir=${userDataDir}`, 
            `--profile-directory=${profileName}`, 
            '--disable-infobars', 
            '--no-sandbox', 
        ],
        turnstile: true,
        headless: false,
        customConfig: {
            userDataDir: userDataDir
        },
        // proxy:{
        //     host:'46.149.135.73',
        //     port: 63622,
        //     username:'EAzGMRSu',
        //     password:'ZiNQDnXh'
        // },
        connectOption: {
            defaultViewport: null
        },
        plugins: [
            require('puppeteer-extra-plugin-click-and-wait')()
        ]
    });
    
    await page.goto(url, { waitUntil: "domcontentloaded" });
    await new Promise(resolve => setTimeout(resolve, 3000));
    const isLogin = await page.$('#signup_button');
    console.log('isLogin',isLogin);
    if (isLogin != null) {
        console.log(`Nút login tồn tại, cần đăng nhập, đóng trình duyệt...`);
        await browser.close();
    } else {
        console.log('Đã đăng nhập, tiếp tục...');
        await new Promise(resolve => setTimeout(resolve, 2000));
        await page.evaluate(async () => {
            for (let i = 0; i < document.body.scrollHeight; i += 100) {
                window.scrollBy(0, 100);
                await new Promise(resolve => setTimeout(resolve, 100)); 
            }
        });
        await new Promise(resolve => setTimeout(resolve, 4000));
        console.log('Đã hoàn tất cuộn trang và chờ 4 giây. Sẵn sàng để nhấn nút.');
        try {
            
            const button = await page.$('#free_play_form_button');
            console.log('button',button);
            if (button == null) {
                  // Nếu nút tồn tại, nhấn nút và chờ 4 giây
                console.log('Nút Roll không tồn tại, đóng trình duyệt...');
            } else {
                console.log('Nút Roll tồn tại, nhấn nút...');
                //await page.click('#free_play_form_button');
                await new Promise(resolve => setTimeout(resolve, 4000));
            }
        } catch (error) {
            console.log('Nút Roll không tồn tại, đóng trình duyệt...');
        }finally {
            console.log('Nút Roll không tồn tại, đóng trình duyệt...');
        }
        await browser.close();
    }
});

test();



