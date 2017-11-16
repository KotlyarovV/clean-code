
namespace Markdown.Readers
{
    public interface IReader
    {
        void ReadChar(int index, string str);
        bool IsActive { get; set; }
    }
}
