using System;
using System.IO;
using System.Text;

namespace ZBW.ProgAdvII.Serialization.BusinessLogic.Exercises._1
{
    public class ZbwSerializer : ISerializer
    {
        public void Serialize(Student student, Stream stream)
        {
            byte[] buffer = new byte[student.FirstName.Length + 1];
            buffer[0] = Convert.ToByte(student.FirstName.Length);
            Encoding.ASCII.GetBytes(student.FirstName).CopyTo(buffer, 1);
            stream.Write(buffer, 0, student.FirstName.Length + 1);

            buffer = new byte[student.LastName.Length + 1];
            buffer[0] = Convert.ToByte(student.LastName.Length);
            Encoding.ASCII.GetBytes(student.LastName).CopyTo(buffer, 1);
            stream.Write(buffer, student.FirstName.Length + 1, student.LastName.Length + 1);

            buffer = new byte[3];
            int daysSince = Convert.ToInt32(student.DateOfBirth);
            BitConverter.GetBytes(daysSince).CopyTo(buffer, 0);
            stream.Write(buffer, student.FirstName.Length + 1 + student.LastName.Length + 1, 3);
            stream.Flush();
        }

        public Student Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}