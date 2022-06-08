/* -------------------------------------------------------------------------------
 * Copyright (c) Suzhou Puman Technology Co., Ltd. All rights reserved.
 * 
 * WebSite: https://www.pumantech.com
 * Contact: knownchen@163.com
 * 
 * Change Logs:
 * Date           Author       Notes
 * 2020-08-20     KnownChen
 * ------------------------------------------------------------------------------- */

Picker.action.icon = {
    title: '选择图标', type: 'component',
    component: new IconPicker(),
    valueField: 'Icon', textField: 'Icon'
}

function IconPicker() {
    var icons = [
        'fa-glass', 'fa-th-large', 'fa-arrow-right', 'fa-arrow-up', 'fa-arrow-down', 'fa-mail-forward',
        'fa-share', 'fa-expand', 'fa-compress', 'fa-plus', 'fa-minus', 'fa-asterisk', 'fa-th',
        'fa-exclamation-circle', 'fa-gift', 'fa-leaf', 'fa-fire', 'fa-eye', 'fa-eye-slash',
        'fa-warning', 'fa-exclamation-triangle', 'fa-plane', 'fa-calendar', 'fa-th-list', 'fa-random',
        'fa-comment', 'fa-magnet', 'fa-chevron-up', 'fa-chevron-down', 'fa-retweet', 'fa-shopping-cart',
        'fa-folder', 'fa-folder-open', 'fa-arrows-v', 'fa-check', 'fa-arrows-h', 'fa-bar-chart-o',
        'fa-bar-chart', 'fa-twitter-square', 'fa-facebook-square', 'fa-camera-retro', 'fa-key',
        'fa-gears', 'fa-cogs', 'fa-comments', 'fa-remove', 'fa-thumbs-o-up', 'fa-thumbs-o-down',
        'fa-star-half', 'fa-heart-o', 'fa-sign-out', 'fa-linkedin-square', 'fa-thumb-tack',
        'fa-external-link', 'fa-sign-in', 'fa-trophy', 'fa-close', 'fa-github-square', 'fa-upload',
        'fa-lemon-o', 'fa-phone', 'fa-square-o', 'fa-bookmark-o', 'fa-phone-square', 'fa-twitter',
        'fa-facebook', 'fa-github', 'fa-times', 'fa-unlock', 'fa-credit-card', 'fa-rss', 'fa-hdd-o',
        'fa-bullhorn', 'fa-bell', 'fa-certificate', 'fa-hand-o-right', 'fa-hand-o-left', 'fa-hand-o-up',
        'fa-search-plus', 'fa-hand-o-down', 'fa-arrow-circle-left', 'fa-arrow-circle-right',
        'fa-arrow-circle-up', 'fa-arrow-circle-down', 'fa-globe', 'fa-wrench', 'fa-tasks', 'fa-filter',
        'fa-briefcase', 'fa-search-minus', 'fa-arrows-alt', 'fa-group', 'fa-users', 'fa-chain',
        'fa-link', 'fa-cloud', 'fa-flask', 'fa-cut', 'fa-scissors', 'fa-copy', 'fa-power-off',
        'fa-files-o', 'fa-paperclip', 'fa-save', 'fa-floppy-o', 'fa-square', 'fa-navicon', 'fa-reorder',
        'fa-bars', 'fa-list-ul', 'fa-list-ol', 'fa-music', 'fa-signal', 'fa-strikethrough',
        'fa-underline', 'fa-table', 'fa-magic', 'fa-truck', 'fa-pinterest', 'fa-pinterest-square',
        'fa-google-plus-square', 'fa-google-plus', 'fa-money', 'fa-gear', 'fa-caret-down',
        'fa-caret-up', 'fa-caret-left', 'fa-caret-right', 'fa-columns', 'fa-unsorted', 'fa-sort',
        'fa-sort-down', 'fa-sort-desc', 'fa-sort-up', 'fa-cog', 'fa-sort-asc', 'fa-envelope',
        'fa-linkedin', 'fa-rotate-left', 'fa-undo', 'fa-legal', 'fa-gavel', 'fa-dashboard',
        'fa-tachometer', 'fa-comment-o', 'fa-trash-o', 'fa-comments-o', 'fa-flash', 'fa-bolt',
        'fa-sitemap', 'fa-umbrella', 'fa-paste', 'fa-clipboard', 'fa-lightbulb-o', 'fa-exchange',
        'fa-cloud-download', 'fa-home', 'fa-cloud-upload', 'fa-user-md', 'fa-stethoscope',
        'fa-suitcase', 'fa-bell-o', 'fa-coffee', 'fa-cutlery', 'fa-file-text-o', 'fa-building-o',
        'fa-hospital-o', 'fa-file-o', 'fa-ambulance', 'fa-medkit', 'fa-fighter-jet', 'fa-beer',
        'fa-h-square', 'fa-plus-square', 'fa-angle-double-left', 'fa-angle-double-right',
        'fa-angle-double-up', 'fa-angle-double-down', 'fa-clock-o', 'fa-angle-left', 'fa-angle-right',
        'fa-angle-up', 'fa-angle-down', 'fa-desktop', 'fa-laptop', 'fa-tablet', 'fa-mobile-phone',
        'fa-mobile', 'fa-circle-o', 'fa-road', 'fa-quote-left', 'fa-quote-right', 'fa-spinner',
        'fa-circle', 'fa-mail-reply', 'fa-reply', 'fa-github-alt', 'fa-folder-o', 'fa-folder-open-o',
        'fa-smile-o', 'fa-download', 'fa-frown-o', 'fa-meh-o', 'fa-gamepad', 'fa-keyboard-o',
        'fa-flag-o', 'fa-flag-checkered', 'fa-terminal', 'fa-code', 'fa-mail-reply-all', 'fa-reply-all',
        'fa-arrow-circle-o-down', 'fa-star-half-empty', 'fa-star-half-full', 'fa-star-half-o',
        'fa-location-arrow', 'fa-crop', 'fa-code-fork', 'fa-unlink', 'fa-chain-broken', 'fa-question',
        'fa-info', 'fa-search', 'fa-arrow-circle-o-up', 'fa-exclamation', 'fa-superscript',
        'fa-subscript', 'fa-eraser', 'fa-puzzle-piece', 'fa-microphone', 'fa-microphone-slash',
        'fa-shield', 'fa-calendar-o', 'fa-fire-extinguisher', 'fa-inbox', 'fa-rocket', 'fa-maxcdn',
        'fa-chevron-circle-left', 'fa-chevron-circle-right', 'fa-chevron-circle-up',
        'fa-chevron-circle-down', 'fa-html5', 'fa-css3', 'fa-anchor', 'fa-unlock-alt',
        'fa-play-circle-o', 'fa-bullseye', 'fa-ellipsis-h', 'fa-ellipsis-v', 'fa-rss-square',
        'fa-play-circle', 'fa-ticket', 'fa-minus-square', 'fa-minus-square-o', 'fa-level-up',
        'fa-level-down', 'fa-rotate-right', 'fa-check-square', 'fa-pencil-square',
        'fa-external-link-square', 'fa-share-square', 'fa-compass', 'fa-toggle-down',
        'fa-caret-square-o-down', 'fa-toggle-up', 'fa-caret-square-o-up', 'fa-toggle-right',
        'fa-repeat', 'fa-caret-square-o-right', 'fa-euro', 'fa-eur', 'fa-gbp', 'fa-dollar', 'fa-usd',
        'fa-rupee', 'fa-inr', 'fa-cny', 'fa-rmb', 'fa-refresh', 'fa-yen', 'fa-jpy', 'fa-ruble',
        'fa-rouble', 'fa-rub', 'fa-won', 'fa-krw', 'fa-bitcoin', 'fa-btc', 'fa-file', 'fa-list-alt',
        'fa-file-text', 'fa-sort-alpha-asc', 'fa-sort-alpha-desc', 'fa-sort-amount-asc',
        'fa-sort-amount-desc', 'fa-sort-numeric-asc', 'fa-sort-numeric-desc', 'fa-thumbs-up',
        'fa-thumbs-down', 'fa-youtube-square', 'fa-lock', 'fa-youtube', 'fa-xing', 'fa-xing-square',
        'fa-youtube-play', 'fa-dropbox', 'fa-stack-overflow', 'fa-instagram', 'fa-flickr', 'fa-adn',
        'fa-bitbucket', 'fa-flag', 'fa-bitbucket-square', 'fa-tumblr', 'fa-tumblr-square',
        'fa-long-arrow-down', 'fa-long-arrow-up', 'fa-long-arrow-left', 'fa-long-arrow-right',
        'fa-apple', 'fa-windows', 'fa-android', 'fa-headphones', 'fa-linux', 'fa-dribbble', 'fa-skype',
        'fa-foursquare', 'fa-trello', 'fa-female', 'fa-male', 'fa-gittip', 'fa-sun-o', 'fa-moon-o',
        'fa-envelope-o', 'fa-volume-off', 'fa-archive', 'fa-bug', 'fa-vk', 'fa-weibo', 'fa-renren',
        'fa-pagelines', 'fa-stack-exchange', 'fa-arrow-circle-o-right', 'fa-arrow-circle-o-left',
        'fa-toggle-left', 'fa-volume-down', 'fa-caret-square-o-left', 'fa-dot-circle-o',
        'fa-wheelchair', 'fa-vimeo-square', 'fa-turkish-lira', 'fa-try', 'fa-plus-square-o',
        'fa-space-shuttle', 'fa-slack', 'fa-envelope-square', 'fa-volume-up', 'fa-wordpress',
        'fa-openid', 'fa-institution', 'fa-bank', 'fa-university', 'fa-mortar-board',
        'fa-graduation-cap', 'fa-yahoo', 'fa-google', 'fa-reddit', 'fa-qrcode', 'fa-reddit-square',
        'fa-stumbleupon-circle', 'fa-stumbleupon', 'fa-delicious', 'fa-digg', 'fa-pied-piper',
        'fa-pied-piper-alt', 'fa-drupal', 'fa-joomla', 'fa-language', 'fa-barcode', 'fa-fax',
        'fa-building', 'fa-child', 'fa-paw', 'fa-spoon', 'fa-cube', 'fa-cubes', 'fa-behance',
        'fa-behance-square', 'fa-steam', 'fa-tag', 'fa-steam-square', 'fa-recycle', 'fa-automobile',
        'fa-car', 'fa-cab', 'fa-taxi', 'fa-tree', 'fa-spotify', 'fa-deviantart', 'fa-soundcloud',
        'fa-tags', 'fa-database', 'fa-file-pdf-o', 'fa-file-word-o', 'fa-file-excel-o',
        'fa-file-powerpoint-o', 'fa-file-photo-o', 'fa-file-picture-o', 'fa-file-image-o',
        'fa-file-zip-o', 'fa-file-archive-o', 'fa-book', 'fa-file-sound-o', 'fa-file-audio-o',
        'fa-file-movie-o', 'fa-file-video-o', 'fa-file-code-o', 'fa-vine', 'fa-codepen', 'fa-jsfiddle',
        'fa-life-bouy', 'fa-life-buoy', 'fa-bookmark', 'fa-life-saver', 'fa-support', 'fa-life-ring',
        'fa-circle-o-notch', 'fa-ra', 'fa-rebel', 'fa-ge', 'fa-empire', 'fa-git-square', 'fa-git',
        'fa-print', 'fa-hacker-news', 'fa-tencent-weibo', 'fa-qq', 'fa-wechat', 'fa-weixin', 'fa-send',
        'fa-paper-plane', 'fa-send-o', 'fa-paper-plane-o', 'fa-history', 'fa-heart', 'fa-camera',
        'fa-circle-thin', 'fa-header', 'fa-paragraph', 'fa-sliders', 'fa-share-alt', 'fa-arrow-left',
        'fa-share-alt-square', 'fa-bomb', 'fa-soccer-ball-o', 'fa-futbol-o', 'fa-tty', 'fa-font',
        'fa-binoculars', 'fa-plug', 'fa-slideshare', 'fa-twitch', 'fa-yelp', 'fa-newspaper-o',
        'fa-wifi', 'fa-calculator', 'fa-paypal', 'fa-google-wallet', 'fa-bold', 'fa-cc-visa',
        'fa-cc-mastercard', 'fa-cc-discover', 'fa-cc-amex', 'fa-cc-paypal', 'fa-cc-stripe',
        'fa-bell-slash', 'fa-bell-slash-o', 'fa-trash', 'fa-copyright', 'fa-italic', 'fa-at',
        'fa-eyedropper', 'fa-paint-brush', 'fa-birthday-cake', 'fa-area-chart', 'fa-pie-chart',
        'fa-line-chart', 'fa-lastfm', 'fa-lastfm-square', 'fa-toggle-off', 'fa-text-height',
        'fa-toggle-on', 'fa-bicycle', 'fa-bus', 'fa-ioxhost', 'fa-angellist', 'fa-cc', 'fa-shekel',
        'fa-sheqel', 'fa-ils', 'fa-meanpath', 'fa-text-width', 'fa-align-left', 'fa-align-center',
        'fa-align-right', 'fa-align-justify', 'fa-star', 'fa-list', 'fa-dedent', 'fa-outdent',
        'fa-indent', 'fa-video-camera', 'fa-photo', 'fa-image', 'fa-picture-o', 'fa-pencil',
        'fa-map-marker', 'fa-star-o', 'fa-adjust', 'fa-tint', 'fa-edit', 'fa-pencil-square-o',
        'fa-share-square-o', 'fa-check-square-o', 'fa-arrows', 'fa-step-backward', 'fa-fast-backward',
        'fa-backward', 'fa-user', 'fa-play', 'fa-pause', 'fa-stop', 'fa-forward', 'fa-fast-forward',
        'fa-step-forward', 'fa-eject', 'fa-chevron-left', 'fa-chevron-right', 'fa-plus-circle',
        'fa-film', 'fa-minus-circle', 'fa-times-circle', 'fa-check-circle', 'fa-question-circle',
        'fa-info-circle', 'fa-crosshairs', 'fa-times-circle-o', 'fa-check-circle-o', 'fa-ban'
    ];
    var selected = '';

    this.render = function (dom) {
        var ul = $('<ul>').addClass('icon-picker').appendTo(dom);
        for (var i = 0; i < icons.length; i++) {
            $('<li>').data('item', icons[i])
                .addClass('fa ' + icons[i])
                .attr('title', icons[i])
                .appendTo(ul)
                .on('click', function () {
                    $('.icon-picker li').removeClass('selected');
                    $(this).addClass('selected');
                    selected = $(this).data('item');
                });
        }
    }

    this.getData = function () {
        return { Icon: 'fa ' + selected };
    }
}