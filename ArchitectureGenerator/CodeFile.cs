public class CodeFile
{
    public CodeFile()
    {
    }

    public CodeFile(string fileName, string path, string templateName)
    {
        FileName = fileName;
        Path = path;
        TemplateName = templateName;
    }

    public string Path { get; set; }
    public string FileName { get; set; }
    public string TemplateName { get; set; }
    public string Code { get; set; }

    public string FullPath => System.IO.Path.Combine(Path, FileName);
}