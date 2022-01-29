using Grades.Models;

namespace Grades
{
    internal interface IFileManager
    {
        void Write(DataModel dataModelInput);
        void Read();
        void Display();
    }
}