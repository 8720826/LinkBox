using LinkBox.Contexts;
using LinkBox.Extentions;

namespace LinkBox.Jobs
{
    public class LinkJob : BackgroundService
    {
        private ILogger<LinkJob> _logger;
        public readonly IHostEnvironment _hostEnvironment;

        public LinkJob(ILogger<LinkJob> logger, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // 执行的方法
                    var checkResult = await CheckLinkIsAvailable();
                    if (!checkResult)
                    {
                        await Task.Delay(1000 * 60);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Job {nameof(LinkJob)} threw an exception!");
                }
                // 间隔的时间
                await Task.Delay(1000);
            }

        }

        private async Task<bool> CheckLinkIsAvailable()
        {
            var db = new LinkboxDbContext();
            var checkTime = DateTime.Now.AddHours(-1);
            var link = db.Links.FirstOrDefault(x => checkTime > x.LastCheckTime);
            if (link != null)
            {
                var uri = await link.Url.CheckAvailableAsync();
                if (uri == null)
                {
                    link.IsAvailable = false;
                }
                else
                {
                    link.IsAvailable = true;
                    link.LastAvailableTime = DateTime.Now;
                }
                link.LastCheckTime = DateTime.Now;
                db.Links.Update(link);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
