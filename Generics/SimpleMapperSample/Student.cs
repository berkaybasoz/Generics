using Infra.Attr;

namespace SimpleMapperSample
{
    internal class Student
    {
        [SimpleMapper(HeaderName = "Id")]
        public int Id { get; set; }
        [SimpleMapper(HeaderName = "Name")]
        public string Name { get; set; }
    }
}