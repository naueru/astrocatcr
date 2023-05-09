

public class PieceVariants
{
    private bool[,] L = { 
        { true, true, true },
        { true, false, false },
        { true, false, false },
    };
    private bool[,] L_INVERTED = {
        { true, false, false },
        { true, false, false },
        { true, true, true },
    };
    private bool[,] U = {
        { true, true, true },
        { true, false, false },
        { true, true, true },
    };
    private bool[,] ROW = {
        { true, false },
        { true, false },
        { true, false }
    };

    public bool[,] GetVariantByName (string name)
    {
        switch (name)
        {
            case "L":
                return L;
            case "L_INVERTED":
                return L_INVERTED;
            case "U":
                return U;
            case "ROW":
                return ROW;
            default:
                return L;                
        }
    }
}
