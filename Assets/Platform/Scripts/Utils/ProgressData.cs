[System.Serializable]
public class ProgressData
{
    public int totalXP;
    public float posX, posY, posZ;
   


    public void saveData(int score, float x, float y, float z)
    {
        totalXP = score;
        posX = x;
        posY = y;
        posZ = z;
    }
}

