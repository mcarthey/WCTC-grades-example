using Grades.Models;

namespace Grades.Dao
{
    internal interface IGradesDao
    {
        void Display();
        void Read();
        void Write(DataModel dataModelInput);
    }
}
