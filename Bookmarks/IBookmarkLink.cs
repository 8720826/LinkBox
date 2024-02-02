namespace LinkBox.Bookmarks
{
    public interface IBookmarkLink : IBookmarkItem
    {
        string Url { get; set; }
    }
}