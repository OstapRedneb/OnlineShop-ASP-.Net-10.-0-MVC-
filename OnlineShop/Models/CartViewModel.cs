using System.Collections;
using System.Collections.Generic;

namespace OnlineShop.Models
{
    public record CartViewModel : IEnumerable<PositionViewModel>
    {
        private readonly List<PositionViewModel> _positions = new List<PositionViewModel>();

        public Guid Id { get; init; }
        public decimal Price => _positions.Sum(position => position.Price);

        public int Count => _positions.Count;

        public Guid UserId;


        //ctor
        public CartViewModel() : this(new List<PositionViewModel>()) 
        { }
        public CartViewModel(List<PositionViewModel> positions) : this(Guid.NewGuid(), positions)
        { }
        public CartViewModel(Guid id, List<PositionViewModel> positions) 
        {
            Id = id;
            _positions = positions;
        }

        public PositionViewModel this[int index]
        {
            get => _positions[index];
            set => _positions[index] = value;
        }

        //Методы интерфейса
        public IEnumerator<PositionViewModel> GetEnumerator() => _positions.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();


        public bool Add(PositionViewModel position) 
        {
            if (position is null)
                return false;

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Product.Id == position.Product.Id) 
                {
                    this[i].Quantity++;
                    return true;
                }
            }

            this._positions.Add(position);
            return true;
        }
        public bool Add(ProductViewModel product) 
        {
            if (product is null)
                return false;

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Product.Id == product.Id)
                {
                    this[i].Quantity++;
                    return true;
                }
            }

            this._positions.Add(new PositionViewModel(product));
            return true;
        }
        public bool Remove(PositionViewModel position) 
        {
            if (position is null || !this.Contains(position)) 
                return false;

            _positions.Remove(position);
            return true;
        }
        public bool Remove(ProductViewModel product) 
        {
            if (product is null || this.All(position => position.Product.Id != product.Id))
                return false;

            PositionViewModel position = this.FirstOrDefault(position => position.Product.Id == product.Id);

            _positions.Remove(position);
            return true;
        }
        public void Clear() 
        {
            _positions.Clear();
        }
    }
}
