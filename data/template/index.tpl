<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@Model.Config.Name - LinkBox</title>
    <style>
        @(Model.Css)
    </style>
</head>
<body class="app-content-uppercase">

    <div class="pageview" id="page-home">

        <div class="container">
            <div class="module-container" id="search-container">
                <form action="https://www.baidu.com/s" method="get" target="_blank">
                    <input name="wd" id="search" autocomplete="off" autofocus placeholder="&#32;">
                    <label for="wd" id="search-label">按回车键百度一下 :)</label>
                </form>
            </div>
            <div class="module-container" id="hero-container">
                <div id="plugin-datetime">
                    <p>
                        <span>@(System.DateTime.Now.ToString("yyyy年MM月dd日 dddd HH:mm:ss"))</span>
                    </p>
                    <h1 class="plugin-container" id="plugin-greetings">Hello World</h1>
                </div>
            </div>



            <div class="plugin-container" id="container-apps">
                @foreach (var cate in Model.AppCategories)
                {
                <h2>@(cate.Name)</h2>
                <div class="apps-container clearfix">
                    @foreach (var link in cate.Links)
                    {
                    <div class="app-container" data-id="evernote">
                        <a target="_blank" rel="noopener" href="@link.Url" class="app-item" title="@link.Title">
                            <div class="app-icon">
                                <img src="@(link.Icon)" alt="@link.Title" />
                            </div>
                            <div class="app-text">
                                <p class="app-title">@link.Title</p>
                                <p class="app-desc">@link.Description</p>
                            </div>
                        </a>
                    </div>
                    }
                </div>
                }

            </div>
            <div class="plugin-container clearfix" id="container-bookmakrs">
                <h2>书签</h2>
                @foreach (var cate in Model.BookmarkCategories)
                {
                <div class="bookmark-group-container pull-left">
                    <h3 class="bookmark-group-title">@(cate.Name)</h3>
                    <ul class="bookmark-list">
                        @foreach (var link in cate.Links)
                        {
                        <li>
                            <a target="_blank" rel="noopener" href="@(link.Url)" class="bookmark">
                                <img src="@(link.Icon)" alt="@link.Title" />
                                <span>@(link.Title)</span>
                            </a>
                        </li>
                        }
                    </ul>
                </div>
                }
            </div>

        </div>


        <footer class="footer-container">
            &copy; @(System.DateTime.Now.Year)
            - <a href="https://github.com/8720826/LinkBox" target="_blank">LinkBox</a>
            - <a href="/dash" target="_blank">后台管理</a>
        </footer>
    </div>


    <script>
        @(Model.Js)
    </script>
</body>
</html>