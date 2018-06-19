using System;
using Xunit;
using Lab03WordGuessGame;
using System.IO;

namespace WordGuessGameTest
{
    public class UnitTest1
    {
        [Fact]
        public void CreateInCrud()
        {
            string[] testArray = new string[] { "thing1", "thing2"};
            Assert.Equal("File made", Program.CreateFile("../../../MyFile.txt", testArray));
        }

        [Fact]
        public void ReadInCrud()
        {
            string[] testRetrun = { "thing1", "thing2" };
            Assert.Equal(testRetrun, Program.GetArrayOfWords());
        }

        [Fact]
        public void UpdateInCrud()
        {
            Assert.Equal("Word added", Program.AddWord("../../../MyFile.txt", "thing3"));
        }

        [Fact]
        public void DeleteInCrud()
        {
            Assert.Equal("File Replaced", Program.DeleteWord("thing3"));
        }

        [Fact]
        public void CheckuserGuessCorrect()
        {
            string testString = "TestWord";
            bool testBool = testString.Contains('t');
            Assert.True(testBool);
        }

        [Fact]
        public void CheckWrongGuess()
        {
            string testString = "TestWord";
            bool testBool = testString.Contains('c');
            Assert.False(testBool);
        }
    }
}
