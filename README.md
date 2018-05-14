# Chemistry Playground AR

## Introduction

This repository is a graduation project for my bachelor degree at Don State Technical University, Russia. It is a small attempt to bring gamification to chemistry classes through AR. By the time when I started to work on this project I had a 1 year production experience with VR and AR projects while working at Yode Group. After that I worked at Yandex.Verticals at Auto.ru Android squad for 6 month already and experienced much of Kotlin development. This project has no much innovation, this is just a result of 7-days marathon of garage-coding during my vacation.

## Interesting parts

The most interesting parts of project are the logic of the subgraph evaluation and extensions file, heavily influenced by my Kotlin experience.

* When I started this project I dreamed that you could use the markers as controls, moving atoms back-and-forth to make molecules and tear them apart, simulating the covalent bindings. But I was upset to learn that detecting an isomorphism of a subgraph is NP problem. So I cheated and changed that to find an isomorphism of a subgraph in a fully connected graph which is much easier.
* Also my previous experience with Kotlin made me to make some extensions to mimic Kotlin-style collection functions. C# Collections are cumbersome even with its LINQ and stuff, also Unity 3D is using outdated runtime, which is missing some useful functions. Some functions like `listOf` saved me a dozens of keystrokes. I wish that I would had these Extensions when I was working with Unity at production.

## Boring parts

### Acknowledgements

This project is **NOT**

* production-ready app
* example of good code quality
* example of good arhitecture

If you read until here, I'm pleased with your curiousity, but please, don't waste any more time and don't look inside sources, you won't find anything useful there.

### License

```
DO WHAT YOU WANT TO PUBLIC LICENSE

 Copyright (C) 2018 Mikhail Levchenko

 Everyone is permitted to copy and distribute verbatim or modified
 copies of this license document, and changing it is allowed as long
 as the name is changed.

            DO WHAT YOU WANT TO PUBLIC LICENSE
TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION

  0. You just DO WHAT YOU WANT TO.
```