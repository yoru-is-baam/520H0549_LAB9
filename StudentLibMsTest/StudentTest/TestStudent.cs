using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentServiceLib;
using System;

namespace StudentTest
{
    [TestClass]
    public class TestStudent
    {
        private Student student;
        private StudentService ss;

        [TestInitialize]
        public void Init()
        {
            student = new Student();
            ss = new StudentService();
        }

        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void ExceptionShouldThrow_WhenScoreExceed10()
        {
            student.Score = 12;
        }

        [TestMethod]
        [ExpectedException(typeof(SystemException))]
        public void ExceptionShouldThrow_WhenScoreLowerThan0()
        {
            student.Score = -1;
        }

        [TestMethod]
        public void ExceptionShouldThrow_RightMessageWhenExceed10()
        {
            String expectedMessage = "Score must not exceed 10.0";
            String message = null;

            try
            {
                student.Score = 12;
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            Assert.AreEqual(expectedMessage, message);
        }

        [TestMethod]
        [DataRow(8, 'A', DisplayName = "Equal 8 should be A")]
        [DataRow(10, 'A', DisplayName = "Equal 10 should be A")]
        [DataRow(7, 'B', DisplayName = "Equal 7 should be B")]
        [DataRow(5, 'C', DisplayName = "Equal 5 should be C")]
        public void TestLetterScore(int score, char expect)
        {
            student.Score = score;

            char letterScore = student.getLetterScore();

            Assert.AreEqual(expect, letterScore);
        }

        [TestMethod]
        public void JustAdd_WhenIdHasNotInList()
        {
            student.Id = 1;
            student.Name = "Kiet";
            student.Age = 12;
            student.Score = 9;

            bool isAdded = ss.addStudent(student);

            Assert.IsTrue(isAdded);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ThrowNullReferenceException_WhenStudentIsNull()
        {
            ss.addStudent(null);
        }

        [TestMethod]
        public void AddSuccessfully_StudentIncrease1_MethodReturnTrue()
        {
            student.Id = 2;
            student.Name = "Kiet";
            student.Age = 12;
            student.Score = 9;

            int originalSize = ss.size();

            bool isAdded = ss.addStudent(student);
            bool isIncrease1AndMethodTrue = isAdded && (ss.size() - originalSize == 1);

            Assert.IsTrue(isIncrease1AndMethodTrue);
        }
    }
}
