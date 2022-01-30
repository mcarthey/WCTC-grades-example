using Grades.Models;

namespace Grades.Dao
{
    internal interface IGradesDao
    {
        void Write(DataModel dataModelInput);
        void Read();
        void Display();
    }
}