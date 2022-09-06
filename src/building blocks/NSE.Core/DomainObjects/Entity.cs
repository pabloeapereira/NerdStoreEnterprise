﻿using NSE.Core.Messages;

namespace NSE.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        private List<Event> _events;

        public IReadOnlyCollection<Event> Events => _events?.AsReadOnly();

        public void AddEvent(Event @event){
            _events ??= new List<Event>();
            _events.Add(@event);
        }

        public void RemoveEvent(Event @event) => _events?.Remove(@event);

        public void ClearEvents() => _events?.Clear();


        #region Comparations
        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;
            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode() =>
            (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString() =>
            $"{GetType().Name} [Id={Id}]";

        public static bool operator == (Entity a, Entity b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b) => !(a == b);
        #endregion
    }
}