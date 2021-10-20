using System;

namespace MyTonaTest.Common
{
  public class ObservableVariable<T>
  {
    public event Action OnChanged;

    private T _value;

    public T Value
    {
      get => _value;
      set
      {
        _value = value;
        OnChanged?.Invoke();
      }
    }

    public ObservableVariable() =>
      Value = default;

    public ObservableVariable(T value) =>
      Value = value;
    
    public override string ToString() =>
      Value.ToString();
  }
}