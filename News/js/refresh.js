﻿var refresh = {}; refresh.settings = { minute: 5 }, refresh.start = function (e) { refresh.settings = Object.assign({}, refresh.settings, e), time = 60 * refresh.settings.minute * 1e3, setInterval(function () { window.location.reload(1) }, time) };