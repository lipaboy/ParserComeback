namespace PupaParserComeback.Presentation.Abstract
{
    public interface IValidCardGroup
    {
        bool IsValid { get; }
        string Error { get; }
    }
}
