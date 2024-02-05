namespace LinkBox.Template
{
    public interface ITemplateService
    {
        string Reset(string file);

        void Update(string file, string content);

        string Read(string file);
    }
}
