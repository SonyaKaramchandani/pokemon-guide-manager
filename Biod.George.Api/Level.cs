using System;


namespace BlueDot.DiseasesAPI
{

    /// <summary>
    /// Level
    /// </summary>
    public class Level
    {
        /// <summary>
        /// Gets the phrase.
        /// </summary>
        /// <value>
        /// The phrase.
        /// </value>
        public string phrase { get; private set; }
        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int level { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Level"/> class.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="l">The l.</param>
        public Level(string p, int l)
        {
            this.phrase = p;
            this.level = l;
        }

        /// <summary>
        /// To the level.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <param name="maxVal">The maximum value.</param>
        /// <param name="levelChoices">The level choices.</param>
        /// <param name="minVal">The minimum value.</param>
        /// <returns></returns>
        public static Level ToLevel(double val, double maxVal, Level[] levelChoices, double minVal = 0.5)
        {
            if (val < minVal)
                return levelChoices[0];
            int numLevels = levelChoices.Length - 1;
            return levelChoices[1 + (int)Math.Min((val - 1) / (maxVal / numLevels), numLevels - 1)];
        }

    } // class Level

}
