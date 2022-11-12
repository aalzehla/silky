module.exports = {
    title: 'Silky Microservice Framework Online Documentation',
    description: 'Silky framework is a .net platform quickly builds a framework for microservice development。stable、Safety、high performance、Easy to expand、Easy-to-use features。',
    port: 8081,
    plugins: [['social-share', {
        networks: ['qq', 'weibo', 'douban', 'wechat', 'email', 'twitter', 'facebook', 'reddit', 'telegram'],
        email: '1029765111@qq.com',
        fallbackImage: 'https://gitee.com/liuhll2/silky/raw/main/docs/.vuepress/public/assets/logo/logo.svg',
        autoQuote: true,
        isPlain: true,
    }], ['@vuepress/active-header-links', {
        sidebarLinkSelector: '.sidebar-link',
        headerAnchorSelector: '.header-anchor'
    }],
        '@vuepress/back-to-top',
        '@vuepress/last-updated',
        '@vuepress/nprogress',
    [
        '@vuepress/pwa', {
            serviceWorker: true,
            updatePopup: true
        }
    ],
    ['@vuepress/search', {
        searchMaxSuggestions: 10
    }],
    [
        'seo', {
            siteTitle: (_, $site) => $site.title,
            title: $page => $page.title,
            description: $page => $page.frontmatter.description,
            author: (_, $site) => $site.themeConfig.author,
            tags: $page => $page.frontmatter.tags,
            twitterCard: _ => 'summary_large_image',
            type: $page => ['articles', 'posts', 'blog', 'silky'].some(folder => $page.regularPath.startsWith('/' + folder)) ? 'article' : 'website',
            url: (_, $site, path) => ($site.themeConfig.domain || '') + path,
            image: ($page, $site) => $page.frontmatter.image && (($site.themeConfig.domain && !$page.frontmatter.image.startsWith('http') || '') + $page.frontmatter.image),
            publishedAt: $page => $page.frontmatter.date && new Date($page.frontmatter.date),
            modifiedAt: $page => $page.lastUpdated && new Date($page.lastUpdated),
        }
    ],
    ['autometa', {
        site: {
            name: 'silky microservice framework',
        },
        canonical_base: 'http://docs.silky-fk.com',
    }],
    ['sitemap', {
        hostname: 'http://docs.silky-fk.com',
        // Exclude pages with no actual content
        exclude: ["/404.html"]
    }
    ]
    ],
    head: [
        [
            "script",
            {},
            `
var _hmt = _hmt || [];
(function() {
  var hm = document.createElement("script");
  hm.src = "https://hm.baidu.com/hm.js?935dc174ecf32301b55bc431ff5f5b1a";
  var s = document.getElementsByTagName("script")[0];
  s.parentNode.insertBefore(hm, s);
})();
            `
        ],
        [
            "meta",
            {
                name: "keywords",
                content: 'silky Microservices,silky Documentation,silkyMicroservices framework,silky docs,Microservices架构,.netMicroservices框架,dotnetcoreMicroservices'
            }
        ],
        [
            "meta",
            {
                name: "viewport",
                content: 'width=device-width, initial-scale=1'
            }
        ],
        ["meta", { name: "baidu-site-verification", content: "code-cAZSIwloPN" }],
        [
            "script",
            {
                src: "/assets/js/autopush-baidu.js"
            }
        ],
        [
            "meta",
            {
                name: "360-site-verification",
                content: "5da0cdaf9aaf8d8972302c8c7ecabb82"
            }
        ],
        [
            "script",
            {
                src: "/assets/js/autopush-360.js"
            }
        ],
        [
            "script",
            {},
            `
(function(){
var src = "https://s.ssl.qhres2.com/ssl/ab77b6ea7f3fbf79.js";
document.write('<script src="' + src + '" id="sozz"><\/script>');
})();
            `
        ],

    ],
    markdown: {
        lineNumbers: true,
        externalLinks:
            { target: '_blank', rel: 'noopener noreferrer' }
    },
    themeConfig: {
        logo: '/assets/logo/logo.png',
        docsRepo: 'liuhll/silky',
        docsBranch: 'main',
        docsDir: 'docs',
        editLinks: true,
        editLinkText: 'Edit current page',
        edit: {
            docsDir: 'src',
        },
        lastUpdated: 'Last update time',
        nav: [
            { text: 'front page', link: '/' },
            { text: 'Documentation', link: '/silky/' },
            { text: 'configure', link: '/config/' },
            { text: 'Source code analysis', link: '/source/' },
            { text: 'blog post', link: '/blog/' },
            {
                text: 'github', link: 'https://github.com/liuhll/silky'
            },
            {
                text: 'gitee', link: 'https://gitee.com/liuhll2/silky'
            },
        ],
        sidebar: {
            '/silky/': [
                {
                    title: 'Introduction',
                    collapsable: false,
                    children: [
                        ''
                    ]

                },
                {
                    title: '开发Documentation',
                    collapsable: false,
                    children: [
                        'dev-docs/quick-start',
                        'dev-docs/templete',
                        'dev-docs/host',
                        'dev-docs/modularity',
                        'dev-docs/appservice-and-serviceentry',
                        'dev-docs/service-registry',
                        'dev-docs/rpc',
                        'dev-docs/service-governance',
                        'dev-docs/link-tracking',
                        'dev-docs/caching',
                        'dev-docs/identity',
                        'dev-docs/distributed-transactions',
                        'dev-docs/object-mapping',
                        'dev-docs/dependency-injection',
                        'dev-docs/lock',
                        'dev-docs/ws',
                        'dev-docs/gateway',
                        'dev-docs/microservice-architecture',
                        'dev-docs/samples',
                        'dev-docs/noun-explanation',

                    ]
                },
            ],
            '/blog/': [
                {
                    title: 'blog post',
                    collapsable: false,
                    children: [
                        'silky-microservice-profile',
                        'transaction-design',
                        'silky-sample',
                        'silky-sample-order'
                    ]
                },

            ],
            '/source/': [{
                title: 'at startup',
                collapsable: false,
                children: [
                    'startup/host',
                    'startup/engine',
                    'startup/modularity',
                    'startup/service-serviceentry',
                    'startup/server'
                ]
            },
            {
                title: 'Runtime',
                collapsable: false,
                children: [
                    'runtime/routing'
                ]
            }],
            '/config/': [
                {
                    collapsable: false,
                    children: [
                        ''
                    ]
                },

            ]
        }
    }
}