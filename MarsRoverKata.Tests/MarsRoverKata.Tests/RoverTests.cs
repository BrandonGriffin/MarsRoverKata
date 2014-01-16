using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MarsRoverKata.Tests
{
    [TestFixture]
    public class RoverTests
    {
        private Planet planet;
        private Rover rover;
        private Controller controller;

        [SetUp]
        public void Setup()
        {
            planet = new Planet(3, 3);
            rover = new Rover(new Coordinate(1, 1), Direction.North, planet);
            controller = new Controller(rover);
        }

        [Test]
        public void RoversCurrentPositionIsSetToStartingPosition()
        {
            Assert.That(rover.CurrentPosition, Is.EqualTo(new Coordinate(1, 1)));
        }

        [Test]
        public void RoversDirectionIsSet()
        {
            Assert.That(rover.Direction, Is.EqualTo(Direction.North));
        }

        [Test]
        public void RoverCanMoveForward()
        {
            controller.ParseCommands("f");
            Assert.That(rover.CurrentPosition, Is.EqualTo(new Coordinate(1, 2)));
        }

        [Test]
        public void RoverCanMoveBackward()
        {
            controller.ParseCommands("b");
            Assert.That(rover.CurrentPosition, Is.EqualTo(new Coordinate(1, 0)));
        }

        [Test]
        public void RoverCanTurnRight()
        {
            controller.ParseCommands("r");
            Assert.That(rover.Direction, Is.EqualTo(Direction.East));
        }

        [Test]
        public void RoverCanTurnLeft()
        {
            controller.ParseCommands("l");
            Assert.That(rover.Direction, Is.EqualTo(Direction.West));
        }

        [Test]
        public void RoverCanTurnRightThenMoveForward()
        {
            controller.ParseCommands("rf");
            Assert.That(rover.CurrentPosition, Is.EqualTo(new Coordinate(2, 1)));
        }

        [Test]
        public void RoverCanTurnRightThenMoveBackward()
        {
            controller.ParseCommands("rb");
            Assert.That(rover.CurrentPosition, Is.EqualTo(new Coordinate(0, 1)));
        }

        [Test]
        public void RoverShouldWrapAroundTheWorld()
        {
            controller.ParseCommands("rff");
            Assert.That(rover.CurrentPosition, Is.EqualTo(new Coordinate(0, 1)));
        }

        [Test]
        public void RoverShouldThrowAnExceptionWhenItEncountersAnObstacle()
        {
            planet.CreateObstacle(new Coordinate(2, 0));
            controller.ParseCommands("ffrf");

            Assert.That(rover.IsObstructed, Is.EqualTo(true));
        }

        [Test]
        public void RoverShouldStopWhenItEncountersAnObstacle()
        {
            planet.CreateObstacle(new Coordinate(2, 0));
            controller.ParseCommands("ffrf");

            Assert.That(rover.CurrentPosition, Is.EqualTo(new Coordinate(1, 0)));
        }

        [Test]
        public void RoverShouldStopWhenItEncountersTheFirstObstacle()
        {
            planet.CreateObstacle(new Coordinate(2, 0));
            planet.CreateObstacle(new Coordinate(1, 1));
            controller.ParseCommands("ffrflfff");

            Assert.That(rover.CurrentPosition, Is.EqualTo(new Coordinate(1, 0)));
        }

        [Test]
        public void RandomRoverShouldEndUpAtOneZero()
        {
            controller.ParseCommands("ffrffflbrrff");
            Assert.That(rover.CurrentPosition, Is.EqualTo(new Coordinate(1, 0)));
        }
    }
}
