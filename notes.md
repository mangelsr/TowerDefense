# Tower Defense notes

## Pathfinding y NavMesh

Pathfinding => Scripts de comportamiento, relativamente simple comparados con ML

La mayoria de algoritmos utilizan programacion dinamica para encontrar el camino entre dos puntos, por ejemplo el juegode Age of Empieres lo usa para movilizar las tropas

El juego de Fear fue elogiado por la complegidad de la IA de los enemigos que buscaban flanquear y rodear al jugador en los enfrentamientos

## Navmesh

Navmesh es la tecnologia de Pathfinding integrada por defecto de Unity

Objetivo
Bake
Script de logica

Object Tab
Filter By:

- Mesh renderer
- Terrain

Navigation static: bool
Navigation area:

- Walkable
- Not walkable
- Jump

Areas:
Navmesh comes with three built in areas: Walkable, Not Walkable, Jump
but you can add more areas for other actions like combat, or climb

We can generate more Agent types with their own config