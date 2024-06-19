namespace BuildingBlocks.Exceptions;

public class InternalServerException :Exception
{

     public InternalServerException(string message):base(message)
    {

    }

 public InternalServerException(string name,object key):base($"Entity\"{name}\"({key})was not found")
      {

      }


}
