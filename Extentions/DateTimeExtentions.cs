

namespace LinkBox.Extentions
{
    public static class DateTimeExtentions
    {
        public static string ToFriendlyTime(this DateTime dateTime)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = DateTime.Now.Subtract(dateTime);
            var second = ts.TotalSeconds;

            if (dateTime == DateTime.MinValue)
            {
                return "无";
            }

            if (second <= 0)
            {
                return "刚刚";
            }
            if (second < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? "1秒前" : ts.Seconds + "秒前";
            }
            if (second < 2 * MINUTE)
            {
                return "1分钟之前";
            }
            if (second < 45 * MINUTE)
            {
                return ts.Minutes + "分钟前";
            }
            if (second < 90 * MINUTE)
            {
                return "1小时前";
            }
            if (second < 24 * HOUR)
            {
                return ts.Hours + "小时前";
            }
            if (second < 48 * HOUR)
            {
                return "昨天";
            }
            if (second < 30 * DAY)
            {
                return ts.Days + " 天之前";
            }
            if (second < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "一个月之前" : months + "月之前";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "一年前" : years + "年前";
            }
        }
    }
}
