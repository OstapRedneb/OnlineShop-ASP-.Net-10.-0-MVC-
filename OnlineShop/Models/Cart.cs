using System.Collections;
using System.Collections.Generic;

namespace OnlineShop.Models
{
    public record Cart : IEnumerable<Position>
    {
        private readonly List<Position> _positions = new List<Position>();

        public Guid Id { get; init; }
        public decimal Price => _positions.Sum(position => position.Price);


        public Cart() : this(new List<Position>()) 
        { }
        public Cart(List<Position> positions)
        {
            Id = Guid.NewGuid();
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
    }
}
