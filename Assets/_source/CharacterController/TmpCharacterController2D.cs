using System;
using UnityEngine;

namespace Game
{
    public class TmpCharacterController2D : CharacterController2DBase
    {
        private sealed class InternalState : ICharacterController2DState
        {
            public Memory<Collider2D> CollisionsInternal { get; set; }
            public Memory<Collider2D> TriggersInternal { get; set; }

            public CollisionFlags CollisionFlags { get; set; }

            public ReadOnlyMemory<Collider2D> Collisions => CollisionsInternal;
            public ReadOnlyMemory<Collider2D> Triggers => TriggersInternal;


            public event Action<ICharacterController2DState> OnStateUpdated;


            public void UpdateState()
            {
                OnStateUpdated?.Invoke(this);
            }
        }


        [SerializeField] private CapsuleCollider2D _capsuleCollider;

        private Transform _tr;
        private readonly InternalState _internalState = new();

        private static ContactFilter2D _collisionsContactFilter;
        private static ContactFilter2D _triggersContactFilter; //tmp
        private static Collider2D[] _collisionsBuffer;
        private static Memory<Collider2D> _collisionsMemBuffer;


        public override ICharacterController2DState State => _internalState;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Init()
        {
            _collisionsContactFilter = new()
            {
                layerMask = 1, //DEBUG ONLY
                useDepth = false,
                useLayerMask = true,
                useTriggers = false,
            };

            _triggersContactFilter = new()
            {
                useLayerMask = false,
                useTriggers = true,
            };

            _collisionsBuffer = new Collider2D[1024 * 8];
            _collisionsMemBuffer = new(_collisionsBuffer);
        }


        private void Awake()
        {
            Physics2D.autoSyncTransforms = true;
            _capsuleCollider = GetComponent<CapsuleCollider2D>();
            _tr = transform;
        }


        public override void Move(Vector2 move)
        {
            _tr.position += (Vector3)move;
            ResolveCollisions();
            DetectTriggers();
            _internalState.UpdateState();
        }

        private void DetectTriggers()
        {
            int count = OverlapCollider(_triggersContactFilter);

            _internalState.TriggersInternal = _collisionsMemBuffer[..count];
        }

        private int DetectCollisions()
        {
            return OverlapCollider(_collisionsContactFilter);
        }

        private int OverlapCollider(ContactFilter2D filter)
        {
            return _capsuleCollider.OverlapCollider(filter, _collisionsBuffer);
        }

        private void ResolveCollisions()
        {
            var collisionsCount = DetectCollisions();

            if (collisionsCount == 0)
                return;

            var mem = _collisionsMemBuffer[..collisionsCount];
            _internalState.CollisionsInternal = mem;
            var span = mem.Span;
            var thisCol = _capsuleCollider;

            //CollisionFlags cflags = 0;

            for (int i = -1; ++i < collisionsCount;)
            {
                var col = span[i];

                if (col == thisCol)
                    continue;

                var dist = col.Distance(thisCol);

                if (dist.isOverlapped)
                {
                    var correction = (Vector3)(dist.pointA - dist.pointB);
                    _tr.position += correction;
                }
            }

            //_internalState.CollisionFlags = cflags;
            //_internalState.UpdateState();
        }
    }
}