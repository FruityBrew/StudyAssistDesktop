using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyAssistModel;

namespace StudyAssistModelTests
{
    [TestClass]
    public class ProblemTests
    {
        [TestMethod]
        [TestCategory("Ctor")]
        public void Default_IsStudy_True()
        {
            XProblem problem = new XProblem();
            
            Assert.IsTrue(problem.IsStudy);
        }

        [TestMethod]
        [TestCategory("Ctor")]
        public void Default_IsAutoRepeat_True()
        {
            XProblem problem = new XProblem();

            Assert.IsTrue(problem.IsAutoRepeate);
        }

        [TestMethod]
        [TestCategory("Ctor")]
        public void Default_StudyLevel_One()
        {
            XProblem problem = new XProblem();

            Assert.AreEqual(problem.StudyLevel, 1);
        }

        [TestMethod]
        [TestCategory("Ctor")]
        public void Default_CreatedDate_Today()
        {
            XProblem problem = new XProblem();

            Assert.AreEqual(problem.CreationDate, DateTime.Today);
        }

        [TestMethod]
        [TestCategory("Ctor")]
        public void Default_AddedToStudy_Today()
        {
            XProblem problem = new XProblem();

            Assert.AreEqual(problem.AddedToStudyDate, DateTime.Today);
        }

        [TestMethod]
        [TestCategory("Ctor")]
        public void Default_RepeatDate_Tomorrow()
        {
            XProblem problem = new XProblem();

            Assert.AreEqual(problem.RepeatDate, DateTime.Today.AddDays(1));
        }

        //[TestMethod]
        //[TestCategory("StudyLevelUp")]
        //public void StudyLevelUp_Inremented()
        //{
        //    XProblem problem = new XProblem();



        //    Assert.AreEqual(problem.AddedToStudyDate, DateTime.Today);
        //}
    }
}
