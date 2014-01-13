﻿using System;

namespace MarsRoverKata
{
    public class Planet
    {
        public Int32 NumberOfRows { get; private set; }
        public Int32 NumberOfColumns { get; private set; }
        public Coordinate ObstaclePosition { get; private set; }

        public Planet(Int32 rows, Int32 columns)
        {
            NumberOfRows = rows;
            NumberOfColumns = columns;
        }

        public void CreateObstacle(Coordinate location)
        {
            ObstaclePosition = location;
        }
    }
}
