using UnityEngine;
using System.Collections;
using NUnit.Framework;

namespace UnityTest 
{
	/// <summary>
	/// Tests for the Arrow class.
	/// </summary>
	/// <author>Marcel Mayer</author>
	public class ArrowTest 
	{
		private Arrow arrow;

		[SetUp]
		public void SetUp()
		{
			arrow = new Arrow ();
		}

		[Test]
		public void TestCalculateAngleDeltaXPositive()
		{
			float expected = 45;
			float actual = arrow.CalculateAngle (100, 100);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestCalculateAngleDeltaXZero()
		{
			float expected = 90;
			float actual = arrow.CalculateAngle (0, 100);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestCalculateAngleDeltaXNegative()
		{
			float expected = 135;
			float actual = arrow.CalculateAngle (-100, 100);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestCalculateAspectRadioAngle()
		{
			float expected = 45;
			float actual = arrow.CalculateAspectRadioAngle (1000, 1000);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestMoveArrowToBorderRightLowerBounds()
		{
			float[] expected = { 8.0f, 13.0f + 1f/3f };
			float[] actual = arrow.MoveArrowToBorder (-arrow.CalculateAspectRadioAngle (Arrow.width, Arrow.height) + 1, 3, 5);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestMoveArrowToBorderRightUpperBounds()
		{
			float[] expected = { 8.0f, 13.0f + 1f/3f};
			float[] actual = arrow.MoveArrowToBorder (arrow.CalculateAspectRadioAngle (Arrow.width, Arrow.height) - 1, 3, 5);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestMoveArrowToBorderUpperLowerBounds()
		{
			float[] expected = { 3.6f, 6.0f };
			float[] actual = arrow.MoveArrowToBorder (arrow.CalculateAspectRadioAngle (Arrow.width, Arrow.height), 3, 5);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestMoveArrowToBorderUpperUpperBounds()
		{
			float[] expected = { 3.6f, 6.0f };
			float[] actual = arrow.MoveArrowToBorder (180 - arrow.CalculateAspectRadioAngle (Arrow.width, Arrow.height) - 1, 3, 5);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestMoveArrowToBorderLeftLowerBounds()
		{
			float[] expected = { 3.6f, 6.0f };
			float[] actual = arrow.MoveArrowToBorder (180 - arrow.CalculateAspectRadioAngle (Arrow.width, Arrow.height), 3, 5);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestMoveArrowToBorderLeftUpperBounds()
		{
			float[] expected = { -8.0f, -13.0f - 1f/3f};
			float[] actual = arrow.MoveArrowToBorder (180 + arrow.CalculateAspectRadioAngle (Arrow.width, Arrow.height) - 1, 3, 5);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestMoveArrowToBorderLowerLowerBounds()
		{
			float[] expected = { -3.6f, -6.0f };
			float[] actual = arrow.MoveArrowToBorder (180 + arrow.CalculateAspectRadioAngle (Arrow.width, Arrow.height), 3, 5);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestMoveArrowToBorderLowerUpperBounds()
		{
			float[] expected = { 3.6f, 6.0f };
			float[] actual = arrow.MoveArrowToBorder (arrow.CalculateAspectRadioAngle (Arrow.width, Arrow.height), 3, 5);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestCheckMapBordersLeftLimitLowerBounds()
		{
			float[] expected = { Arrow.leftLimit, 5.0f };
			float[] actual = new float[]{ Arrow.leftLimit, 5.0f };
			arrow.CheckMapBorders (ref actual);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestCheckMapBordersLeftLimitUpperBounds()
		{
			float[] expected = { Arrow.leftLimit + 1, 5.0f };
			float[] actual = new float[]{ Arrow.leftLimit + 1, 5.0f };
			arrow.CheckMapBorders (ref actual);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestCheckMapBordersRightLimitLowerBounds()
		{
			float[] expected = { Arrow.rightLimit - 1, 5.0f };
			float[] actual = new float[]{ Arrow.rightLimit - 1, 5.0f };
			arrow.CheckMapBorders (ref actual);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestCheckMapBordersRightLimitUpperBounds()
		{
			float[] expected = { Arrow.rightLimit, 5.0f };
			float[] actual = new float[]{ Arrow.rightLimit, 5.0f };
			arrow.CheckMapBorders (ref actual);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestCheckMapBordersLowerLimitLowerBounds()
		{
			float[] expected = { 5.0f, Arrow.lowerLimit };
			float[] actual = new float[]{ 5.0f, Arrow.lowerLimit };
			arrow.CheckMapBorders (ref actual);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestCheckMapBordersLowerLimitUpperBounds()
		{
			float[] expected = { 5.0f, Arrow.lowerLimit + 1 };
			float[] actual = new float[]{ 5.0f, Arrow.lowerLimit + 1 };
			arrow.CheckMapBorders (ref actual);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestCheckMapBordersUpperLimitLowerBounds()
		{
			float[] expected = { 5.0f, Arrow.upperLimit - 1 };
			float[] actual = new float[]{ 5.0f, Arrow.upperLimit - 1 };
			arrow.CheckMapBorders (ref actual);

			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void TestCheckMapBordersUpperLimitUpperBounds()
		{
			float[] expected = { 5.0f, Arrow.upperLimit };
			float[] actual = new float[]{ 5.0f, Arrow.upperLimit };
			arrow.CheckMapBorders (ref actual);

			Assert.AreEqual (expected, actual);
		}

		[TearDown]
		public void TearDown()
		{
			arrow = null;
		}

	}
}
