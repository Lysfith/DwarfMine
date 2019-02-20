using DwarfMine.Managers;
using DwarfMine.Scenes.SceneImplementation.Game.Components;
using DwarfMine.Services;
using DwarfMine.States.StateImplementation.Game;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfMine.Scenes.SceneImplementation.Game.Systems
{
    public class MovementSystem : EntityUpdateSystem
    {
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<MovementDestination> _movementDestinationMapper;

        private Random _random;

        public MovementSystem()
            : base(Aspect.All(typeof(Transform2), typeof(MovementDestination)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _movementDestinationMapper = mapperService.GetMapper<MovementDestination>();

            _random = new Random();
        }

        public override void Update(GameTime gameTime)
        {
            var elapsedSeconds = gameTime.GetElapsedSeconds();

            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity);
                var movementDestination = _movementDestinationMapper.Get(entity);

                if (movementDestination.Position != null)
                {
                    if(movementDestination.JobComputePath != null)
                    {
                        continue;
                    }

                    var position = transform.WorldPosition;
                    var positionPoint = new Point((int)transform.WorldPosition.X, (int)transform.WorldPosition.Y);
                    var hasReachedDestination = Vector2.Distance(movementDestination.Position.Value.ToVector2(), positionPoint.ToVector2()) < 1.5f;

                    if (hasReachedDestination)
                    {
                        movementDestination.Position = null;
                        movementDestination.Path.Clear();
                        continue;
                    }
                    else
                    {
                        if (movementDestination.Path == null || !movementDestination.Path.Any())
                        {
                            movementDestination.JobComputePath = JobManager.Instance.AddJob(() =>
                            {
                                movementDestination.Path = CreatePath(positionPoint, movementDestination.Position.Value, movementDestination.Path);

                                if (movementDestination.Path.Any())
                                {
                                    movementDestination.Position = movementDestination.Path.Last();
                                }
                                else
                                {
                                    movementDestination.Position = null;
                                }

                                movementDestination.JobComputePath = null;
                            });
                        }
                        else
                        {
                            var nextDestination = movementDestination.Path.Peek();

                            if ((nextDestination.ToVector2() - position).Length() < 1f)
                            {
                                movementDestination.Path.Dequeue();
                            }
                            else
                            {
                                var direction = (nextDestination.ToVector2() - position);

                                direction /= direction.Length();

                                transform.Position = transform.Position + direction * (float)elapsedSeconds * 100f;
                            }
                        }
                    }
                }
                else if(!movementDestination.HasMoved)
                {
                    var newPoint = new Point(_random.Next(10, Constants.REGION_WIDTH * Constants.TILE_WIDTH-10), _random.Next(10, Constants.REGION_HEIGHT * Constants.TILE_HEIGHT-10));

                    var region = MapSystem.Instance.GetRegion((int)newPoint.X, (int)newPoint.Y);
                    var cellIndex = region.GetCellIndexFromWorldPosition((int)newPoint.X, (int)newPoint.Y);
                    var isFree = !region.GetCollision(cellIndex.X, cellIndex.Y);

                    if (isFree)
                    {
                        movementDestination.Position = newPoint;

                        movementDestination.HasMoved = true;
                    }
                }
                else if (movementDestination.HasMoved)
                {
                    _movementDestinationMapper.Delete(entity);
                }
            }
        }

        private Queue<Point> CreatePath(Point position, Point destination, Queue<Point> path = null)
        {
            if (path == null)
            {
                path = new Queue<Point>();
            }
            else
            {
                path.Clear();
            }

            var region = MapSystem.Instance.GetRegion(position.X, position.Y);

            var costs = region.GetCosts();

            var destinationInRegion = region.GetCellIndexFromWorldPosition(destination.X, destination.Y);

            var flows = PathFindingService.ComputeFlow(costs, destinationInRegion.X, destinationInRegion.Y);

            var positionInRegion = region.GetCellIndexFromWorldPosition(position.X, position.Y);
            var positionInWorld = region.GetCellPositionFromWorldPosition(position.X, position.Y);

            path.Enqueue(positionInWorld);

            while (destination != positionInWorld)
            {
                var direction = flows[positionInRegion.X, positionInRegion.Y];

                if (direction == Vector2.Zero)
                {
                    break;
                }

                positionInRegion = (positionInRegion.ToVector2() + direction).ToPoint();
                var newPosition = region.GetCellPositionFromCellIndex(positionInRegion.X, positionInRegion.Y);

                positionInWorld = region.GetCellPositionFromWorldPosition((int)newPosition.X, (int)newPosition.Y);

                path.Enqueue(positionInWorld);
            }

            return path;
        }
    }
}
