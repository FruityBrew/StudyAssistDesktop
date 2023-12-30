﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyAssistInterfaces;
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
            IProblem problem = new XProblem();
            
            Assert.IsTrue(problem.IsStudy);
        }

        [TestMethod]
        [TestCategory("Ctor")]
        public void Default_IsAutoRepeat_True()
        {
            IProblem problem = new XProblem();

            Assert.IsTrue(problem.IsAutoRepeate);
        }

        [TestMethod]
        [TestCategory("Ctor")]
        public void Default_StudyLevel_One()
        {
            IProblem problem = new XProblem();

            Assert.AreEqual(problem.StudyLevel, 1);
        }

        [TestMethod]
        [TestCategory("Ctor")]
        public void Default_CreatedDate_Today()
        {
            IProblem problem = new XProblem();

            Assert.AreEqual(problem.CreationDate, DateTime.Today);
        }

        [TestMethod]
        [TestCategory("Ctor")]
        public void Default_AddedToStudy_Today()
        {
            IProblem problem = new XProblem();

            Assert.AreEqual(problem.AddedToStudyDate, DateTime.Today);
        }

        [TestMethod]
        [TestCategory("Ctor")]
        public void Default_RepeatDate_Tomorrow()
        {
            IProblem problem = new XProblem();

            Assert.AreEqual(problem.RepeatDate, DateTime.Today.AddDays(1));
        }

        [TestMethod]
        [TestCategory("MoveToTomorrow")]
        public void MoveToTomorrow_RepeatDateTomorrow()
        {
            IProblem problem = new XProblem();

            problem.MoveToTomorrow();

            Assert.AreEqual(
                problem.RepeatDate.Value, 
                DateTime.Today.AddDays(1));
        }

        [TestMethod]
        [TestCategory("MoveToTomorrow")]
        public void MoveToTomorrow_RepeatDateTomorrow_1()
        {
            IProblem problem = new XProblem();

            //произвол
            problem.StudyLevelUp();
            problem.StudyLevelUp();
            problem.StudyLevelUp();

            problem.MoveToTomorrow();

            Assert.AreEqual(
                problem.RepeatDate.Value,
                DateTime.Today.AddDays(1));
        }

        [TestMethod]
        [TestCategory("RemoveFromStudy")]
        public void RemoveFromStudy_IsStudyFalse()
        {
            IProblem problem = new XProblem();

            problem.RemoveFromStudy();

            Assert.IsFalse(problem.IsStudy);
        }

        [TestMethod]
        [TestCategory("RemoveFromStudy")]
        public void RemoveFromStudy_RepeatDateNull()
        {
            IProblem problem = new XProblem();

            problem.RemoveFromStudy();

            Assert.IsFalse(problem.RepeatDate.HasValue);
        }

        [TestMethod]
        [TestCategory("RemoveFromStudy")]
        public void RemoveFromStudy_StudyLevelNotChanged()
        {
            IProblem problem = new XProblem();

            problem.StudyLevelUp();
            problem.StudyLevelUp();
            problem.StudyLevelUp();

            var level = problem.StudyLevel;

            problem.RemoveFromStudy();

            Assert.AreEqual(problem.StudyLevel, level);
        }

        [TestMethod]
        [TestCategory("ResetLevel")]
        public void ResetLevel_StudyLevelZero()
        {
            IProblem problem = new XProblem();

            problem.StudyLevelUp();
            problem.StudyLevelUp();
            problem.StudyLevelUp();

            problem.ResetLevel();

            Assert.AreEqual(problem.StudyLevel, 0);
        }

        [TestMethod]
        [TestCategory("AddToStudy")]
        public void AddToStudy_NullArgument()
        {
            IProblem problem = new XProblem();

            problem.RemoveFromStudy();

            Assert.ThrowsException<ArgumentNullException>(
                () => problem.AddToStudy(null));
        }

        [TestMethod]
        [TestCategory("AddToStudy(date)")]
        public void AddToStudy_IsStudyTrue_TrowsEx()
        {
            IProblem problem = new XProblem();

            Assert.ThrowsException<InvalidOperationException>(
                () => problem.AddToStudy(null));
        }

        [TestMethod]
        [TestCategory("AddToStudy(date)")]
        public void AddToStudy_AddeDate_Today()
        {
            IProblem problem = new XProblem();

            problem.RemoveFromStudy();

            problem.AddToStudy(DateTime.Today.AddDays(1));

            Assert.AreEqual(problem.AddedToStudyDate.Value, DateTime.Today);
        }

        [TestMethod]
        [TestCategory("AddToStudy(date)")]
        public void AddToStudy_IsAutoRepeat_False()
        {
            IProblem problem = new XProblem();

            problem.RemoveFromStudy();

            problem.AddToStudy(DateTime.Today.AddDays(1));

            Assert.IsFalse(problem.IsAutoRepeate);
        }

        [TestMethod]
        [TestCategory("AddToStudy(date)")]
        public void AddToStudy_IsStudy_True()
        {
            IProblem problem = new XProblem();

            problem.RemoveFromStudy();

            problem.AddToStudy(DateTime.Today.AddDays(1));

            Assert.IsTrue(problem.IsStudy);
        }

        [TestMethod]
        [TestCategory("AddToStudy(date)")]
        public void AddToStudy_RepeateDate_Date()
        {
            IProblem problem = new XProblem();

            problem.RemoveFromStudy();

            problem.AddToStudy(DateTime.Today.AddDays(1));

            Assert.AreEqual(problem.RepeatDate, DateTime.Today.AddDays(1));
        }

        [TestMethod]
        [TestCategory("AddToStudy(level)")]
        public void AddToStudy_AddedToStudyDate_Today()
        {
            IProblem problem = new XProblem();

            problem.RemoveFromStudy();

            problem.AddToStudy(1);

            Assert.AreEqual(problem.AddedToStudyDate, DateTime.Today);
        }

        [TestMethod]
        [TestCategory("AddToStudy(level)")]
        public void AddToStudy_StudyLevele_Level()
        {
            IProblem problem = new XProblem();

            problem.RemoveFromStudy();

            problem.AddToStudy(2);

            Assert.AreEqual(problem.StudyLevel, 2);
        }

        [TestMethod]
        [TestCategory("AddToStudy(level)")]
        public void AddToStudy_IsAutoRepeate_True()
        {
            IProblem problem = new XProblem();

            problem.RemoveFromStudy();

            problem.AddToStudy(2);

            Assert.IsTrue(problem.IsAutoRepeate);
        }

        [TestMethod]
        [TestCategory("AddToStudy(level)")]
        public void AddToStudyLevel_IsStudy_True()
        {
            IProblem problem = new XProblem();

            problem.RemoveFromStudy();

            problem.AddToStudy(2);

            Assert.IsTrue(problem.IsStudy);
        }

        [TestMethod]
        [TestCategory("AddToStudy(level)")]
        public void AddToStudyLevel_RepeatDate()
        {
            IProblem problem = new XProblem();
            RepeatDateCalculator rp = new RepeatDateCalculator();

            problem.RemoveFromStudy();

            problem.AddToStudy(2);

            Assert.AreEqual(
                problem.RepeatDate, 
                DateTime.Today.AddDays(rp.SecondRepeatPeriod));
        }

        [TestMethod]
        [TestCategory("StudyLevelUp")]
        public void StudyLevelUp_Incremented_1()
        {
            IProblem problem = new XProblem();
            RepeatDateCalculator rp = new RepeatDateCalculator();

            var stLevel = problem.StudyLevel;

            problem.StudyLevelUp();

            Assert.AreEqual(problem.StudyLevel, stLevel + 1);
        }

        [TestMethod]
        [TestCategory("StudyLevelDown")]
        public void StudyLevelDown_StudyLevel_UpDown()
        {
            IProblem problem = new XProblem();

            var stLevel = problem.StudyLevel;

            problem.StudyLevelUp();
            problem.StudyLevelUp();

            problem.StudyLevelDown();
            problem.StudyLevelDown();

            Assert.AreEqual(problem.StudyLevel, stLevel);
        }

        [TestMethod]
        [TestCategory("StudyLevelDown")]
        public void StudyLevelDown_RepeateDate_UpDown()
        {
            IProblem problem = new XProblem();

            var date = problem.RepeatDate;

            problem.StudyLevelUp();
            problem.StudyLevelUp();

            problem.StudyLevelDown();
            problem.StudyLevelDown();

            Assert.AreEqual(problem.RepeatDate, date);
        }

        // не должна поменяться дата повторения, поскольку 
        // она больше сегодняшней
        [TestMethod]
        [TestCategory("StudyLevelUp")]
        public void StudyLevelUp_Incremented_DateNotChanged()
        {
            IProblem problem = new XProblem();
            RepeatDateCalculator rp = new RepeatDateCalculator();

            problem.StudyLevelUp();

            Assert.AreEqual(problem.RepeatDate,problem.RepeatDate);
        }

        [TestMethod]
        [TestCategory("StudyLevelUp")]
        public void StudyLevelUp_Incremented_Date_1()
        {
            IProblem problem = new XProblem();
            RepeatDateCalculator rp = new RepeatDateCalculator();

            // имитируем
            DateTime referenceDate = DateTime.Today.AddDays(-100);
            problem.RepeatDate = referenceDate;

            problem.StudyLevelUp();

            Assert.AreEqual(
                problem.RepeatDate, 
                referenceDate.AddDays(rp.SecondRepeatPeriod));
        }

        [TestMethod]
        [TestCategory("StudyLevelUp")]
        public void StudyLevelUp_Incremented_Date_2()
        {
            IProblem problem = new XProblem();
            RepeatDateCalculator rp = new RepeatDateCalculator();

            // имитируем
            DateTime referenceDate = DateTime.Today.AddDays(-100);
            problem.RepeatDate = referenceDate;

            problem.StudyLevelUp();
            problem.StudyLevelUp();

            referenceDate = referenceDate.AddDays(
                rp.SecondRepeatPeriod + rp.ThirdRepeatPeriod);

            Assert.AreEqual(problem.RepeatDate, referenceDate);
        }


        [TestMethod]
        [TestCategory("StudyLevelUp")]
        public void StudyLevelUp_Incremented_Date_3()
        {
            IProblem problem = new XProblem();
            RepeatDateCalculator rp = new RepeatDateCalculator();

            // имитируем
            DateTime referenceDate = DateTime.Today.AddDays(-100);
            problem.RepeatDate = referenceDate;

            problem.StudyLevelUp();
            problem.StudyLevelUp();
            problem.StudyLevelUp();

            referenceDate = referenceDate.AddDays(
                rp.SecondRepeatPeriod 
                + rp.ThirdRepeatPeriod
                + rp.FourthRepeatPeriod);

            Assert.AreEqual(problem.RepeatDate, referenceDate);
        }

        [TestMethod]
        [TestCategory("StudyLevelUp")]
        public void StudyLevelUp_Incremented_Date_4()
        {
            IProblem problem = new XProblem();
            RepeatDateCalculator rp = new RepeatDateCalculator();

            // имитируем
            DateTime referenceDate = DateTime.Today.AddDays(-100);
            problem.RepeatDate = referenceDate;

            problem.StudyLevelUp();
            problem.StudyLevelUp();
            problem.StudyLevelUp();
            problem.StudyLevelUp();

            referenceDate = referenceDate.AddDays(
                rp.SecondRepeatPeriod
                + rp.ThirdRepeatPeriod
                + rp.FourthRepeatPeriod
                + rp.FifthRepeatPeriod);

            Assert.AreEqual(problem.RepeatDate, referenceDate);
        }

        [TestMethod]
        [TestCategory("StudyLevelUp")]
        public void StudyLevelUp_Incremented_Date_5()
        {
            IProblem problem = new XProblem();
            RepeatDateCalculator rp = new RepeatDateCalculator();

            // имитируем
            DateTime referenceDate = DateTime.Today.AddDays(-100);
            problem.RepeatDate = referenceDate;

            problem.StudyLevelUp();
            problem.StudyLevelUp();
            problem.StudyLevelUp();
            problem.StudyLevelUp();
            problem.StudyLevelUp();

            referenceDate = referenceDate.AddDays(
                rp.SecondRepeatPeriod
                + rp.ThirdRepeatPeriod
                + rp.FourthRepeatPeriod
                + rp.FifthRepeatPeriod
                + rp.DefaultRepeatPeriod);

            Assert.AreEqual(problem.RepeatDate, referenceDate);
        }


        [TestMethod]
        [TestCategory("StudyLevelUp")]
        public void StudyLevelUp_IsNotStudy()
        {
            IProblem problem = new XProblem();

            var level = problem.StudyLevel;

            problem.IsStudy = false;
            problem.StudyLevelUp();

            Assert.AreEqual(problem.StudyLevel, level);
        }
    }
}
