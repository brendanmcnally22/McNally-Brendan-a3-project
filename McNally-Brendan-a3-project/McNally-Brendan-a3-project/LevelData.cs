using System.Numerics;


namespace MohawkGame2D;
public class LevelData
{
    public Platform[] Level1Platforms; // storing each levels platforms in data arrays
    public Platform[] Level2Platforms;
    public Platform[] Level3Platforms;

    public LevelData()
    {

        Level1Platforms = new Platform[] // drawing level 1 platforms 
        {
            new Platform(new Vector2(150, 450), new Vector2(100,50)) // first platform
        };

        Level2Platforms = new Platform[]
        {
            //dont know positions yet
        };

        Level3Platforms = new Platform[]
        {

            //dont know positions yet
        };

    }
}
                


    

