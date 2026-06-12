using System.Collections;
using System.Collections.Generic;

namespace OnlineShop.Models
{
    public record Cart : IEnumerable<Position>
    {
        private readonly List<Position> _positions = new List<Position>();

        public Guid Id { get; init; }
        public decimal Price => _positions.Sum(position => position.Price);

        public int Count => _positions.Count;


        //ctor
        public Cart() : this(new List<Position>()) 
        { }
        public Cart(List<Position> positions) : this(Guid.NewGuid(), positions)
        { }
        public Cart(Guid id, List<Position> positions) 
        {
            Id = id;
            _positions = positions;
        }

        public Position this[int index]
        {
            get => _positions[index];
            set => _positions[index] = value;
        }

        //Методы интерфейса
        public IEnumerator<Position> GetEnumerator() => _positions.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();


        public bool Add(Position position) 
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
        public bool Add(Product product) 
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

            this._positions.Add(new Position(product));
            return true;
        }
        public void Clear() 
        {
            _positions.Clear();
        }
    }
}
