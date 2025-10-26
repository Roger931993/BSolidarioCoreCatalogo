using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Catalog.Domain.Common.Dapper
{
  public abstract class Entity
  {
    public int? _requestedHashCode;

    [NotMapped]
    public virtual int? Id { get; protected set; } = null;

    public bool IsTransient()
    {
      return Id == 0;
    }

    public override bool Equals(object? obj)
    {
      if (obj == null || !(obj is Entity))
      {
        return false;
      }
      if (this == obj)
      {
        return true;
      }

      if (GetType() != obj.GetType())
      {
        return false;
      }

      Entity entity = (Entity)obj;
      if (entity.IsTransient() || IsTransient())
      {
        return false;
      }

      return entity.Id == Id;
    }

    public override int GetHashCode()
    {
      if (!IsTransient())
      {
        if (!_requestedHashCode.HasValue)
        {
          _requestedHashCode = Id.GetHashCode() * 0x1F;
        }
        return _requestedHashCode.Value;
      }
      return base.GetHashCode();
    }

    public static bool operator ==(Entity left, Entity rigth)
    {
      if (object.Equals(left, null))
      {
        return object.Equals(rigth, null) ? true : false;
      }
      return left.Equals(rigth);
    }

    public static bool operator !=(Entity left, Entity rigth)
    {
      return !(left == rigth);
    }
  }
}
