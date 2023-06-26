

public class PieceVariants
{
    private bool[,] L = {
        { true, true, true },
        { true, false, false },
        { true, false, false },
    };
    private bool[,] L_INVERTED = {
        { true, true, true },
        { false, false, true },
        { false, false, true },
    };
    private bool[,] U = {
        { true, true, true },
        { true, false, true },
        { true, false, true },
    };
    private bool[,] ROW = {
        { true, true, true },
        { false, false, false },
    };
    private bool[,] T = {
        { true, true, true },
        { false, true, false },
        { false, true, false },
        { false, true, false },
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
            case "T":
                return T;
            default:
                return L;
        }
    }
}
