namespace Stuff
{
    public interface IProductBuilder
    {
        void setName(string name);
        void setNumber(int number);
        Product GetProduct();

    }
}