public enum Colorr
{
    blue,
    red,
}

[System.Serializable]
public class Questions 
{
    //public string level;
    //public int soluongchuoi;
    public type[] chainArr;
    public bool[] color;
   // public Colorr colorr;
    //public Chair[] listChair;
}

[System.Serializable]
public class Chair
{
   
    public Colorr[] color;
    public type[] shape;
    
}

