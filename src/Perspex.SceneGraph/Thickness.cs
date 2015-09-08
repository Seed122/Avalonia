﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Linq;

namespace Perspex
{
    /// <summary>
    /// Describes the thickness of a frame around a rectangle.
    /// </summary>
    public struct Thickness
    {
        /// <summary>
        /// The thickness on the left.
        /// </summary>
        private double _left;

        /// <summary>
        /// The thickness on the top.
        /// </summary>
        private double _top;

        /// <summary>
        /// The thickness on the right.
        /// </summary>
        private double _right;

        /// <summary>
        /// The thickness on the bottom.
        /// </summary>
        private double _bottom;

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> structure.
        /// </summary>
        /// <param name="uniformLength">The length that should be applied to all sides.</param>
        public Thickness(double uniformLength)
        {
            Contract.Requires<ArgumentException>(uniformLength >= 0);

            _left = _top = _right = _bottom = uniformLength;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> structure.
        /// </summary>
        /// <param name="horizontal">The thickness on the left and right.</param>
        /// <param name="vertical">The thickness on the top and bottom.</param>
        public Thickness(double horizontal, double vertical)
        {
            Contract.Requires<ArgumentException>(horizontal >= 0);
            Contract.Requires<ArgumentException>(vertical >= 0);

            _left = _right = horizontal;
            _top = _bottom = vertical;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> structure.
        /// </summary>
        /// <param name="left">The thickness on the left.</param>
        /// <param name="top">The thickness on the top.</param>
        /// <param name="right">The thickness on the right.</param>
        /// <param name="bottom">The thickness on the bottom.</param>
        public Thickness(double left, double top, double right, double bottom)
        {
            Contract.Requires<ArgumentException>(left >= 0);
            Contract.Requires<ArgumentException>(top >= 0);
            Contract.Requires<ArgumentException>(right >= 0);
            Contract.Requires<ArgumentException>(bottom >= 0);

            _left = left;
            _top = top;
            _right = right;
            _bottom = bottom;
        }

        /// <summary>
        /// Gets the thickness on the left.
        /// </summary>
        public double Left
        {
            get { return _left; }
        }

        /// <summary>
        /// Gets the thickness on the top.
        /// </summary>
        public double Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the thickness on the right.
        /// </summary>
        public double Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the thickness on the bottom.
        /// </summary>
        public double Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets a value indicating whether all sides are set to 0.
        /// </summary>
        public bool IsEmpty
        {
            get { return this.Left == 0 && this.Top == 0 && this.Right == 0 && this.Bottom == 0; }
        }

        /// <summary>
        /// Compares two Thicknesses.
        /// </summary>
        /// <param name="a">The first thickness.</param>
        /// <param name="b">The second thickness.</param>
        /// <returns>The equality.</returns>
        public static bool operator ==(Thickness a, Thickness b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Compares two Thicknesses.
        /// </summary>
        /// <param name="a">The first thickness.</param>
        /// <param name="b">The second thickness.</param>
        /// <returns>The unequality.</returns>
        public static bool operator !=(Thickness a, Thickness b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Adds two Thicknesses.
        /// </summary>
        /// <param name="a">The first thickness.</param>
        /// <param name="b">The second thickness.</param>
        /// <returns>The equality.</returns>
        public static Thickness operator +(Thickness a, Thickness b)
        {
            return new Thickness(
                a.Left + b.Left,
                a.Top + b.Top,
                a.Right + b.Right,
                a.Bottom + b.Bottom);
        }

        /// <summary>
        /// Parses a <see cref="Thickness"/> string.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <returns>The <see cref="Thickness"/>.</returns>
        public static Thickness Parse(string s)
        {
            var parts = s.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();

            switch (parts.Count)
            {
                case 1:
                    var uniform = double.Parse(parts[0]);
                    return new Thickness(uniform);
                case 2:
                    var horizontal = double.Parse(parts[0]);
                    var vertical = double.Parse(parts[1]);
                    return new Thickness(horizontal, vertical);
                case 4:
                    var left = double.Parse(parts[0]);
                    var top = double.Parse(parts[1]);
                    var right = double.Parse(parts[2]);
                    var bottom = double.Parse(parts[3]);
                    return new Thickness(left, top, right, bottom);
            }

            throw new FormatException("Invalid Thickness.");
        }

        /// <summary>
        /// Checks for equality between a thickness and an object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// True if <paramref name="obj"/> is a size that equals the current size.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is Thickness)
            {
                Thickness other = (Thickness)obj;
                return this.Left == other.Left &&
                       this.Top == other.Top &&
                       this.Right == other.Right &&
                       this.Bottom == other.Bottom;
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for a <see cref="Thickness"/>.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (hash * 23) + this.Left.GetHashCode();
                hash = (hash * 23) + this.Top.GetHashCode();
                hash = (hash * 23) + this.Right.GetHashCode();
                hash = (hash * 23) + this.Bottom.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns the string representation of the thickness.
        /// </summary>
        /// <returns>The string representation of the thickness.</returns>
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", _left, _top, _right, _bottom);
        }
    }
}
