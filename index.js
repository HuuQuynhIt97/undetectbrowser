
const { connect } = require("puppeteer-real-browser")
async function test() {
    const { browser, page } = await connect({
        headless: false,
        args: [
            "--no-sandbox",        // Thêm tham số này để tránh các vấn đề với sandbox
            "--disable-setuid-sandbox",
            "--disable-gpu",       // Tắt GPU nếu cần
            "--window-size=1200,800" // Thiết lập kích thước cửa sổ nếu cần
        ],
        customConfig: {},
        turnstile: true,
        connectOption: {},
        disableXvfb: false,
        ignoreAllFlags: false

    })
    await page.goto('https://freebitco.in/')
}
test()