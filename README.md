# BuildingPlayfullWorlds
BPW topdown shooter



## Enemy behaviour

```mermaid
    stateDiagram
    direction TB

    Idle --> Patrol : Timer = 0
    Idle --> Chase : Distance player < 10
    Idle --> Attack : Distance player < 3
    Idle --> Death : Health < 0

    Patrol --> Idle : Waypoint reached
    Patrol --> Chase : Distance player < 10
    Patrol --> Attack : Distance player < 3
    Patrol --> Death : Health < 0

    Chase --> Idle : Distance player > 15
    Chase --> Attack : Distance player < 3
    Chase --> Death : Health < 0

    Attack --> Idle : Distance player > 15
    Attack --> Death : Health < 0
```
