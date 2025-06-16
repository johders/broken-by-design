# Broken By Design

Some things are made to work. This one's made to break, so I can learn how to fix it.

A multi-API .NET application designed to function as an event meetup platform while serving as a practical lab for exploring and mitigating security vulnerabilities.

The primary goal of this project is to deepen my understanding of modern, distributed .NET systems by building one from the ground up, with a special focus on exploring and mitigating common security vulnerabilities.

---

⚠️ Heads up: this project's under construction. Rough spots, broken parts, and a whole lot of lessons along the way. Strap in.

---

## Project Structure  
### The *RendezVoulns* Directory — the main API

What if *"Rendezvous"* and *"Vulnerabilities"* had a weird-looking baby, and I decided to raise it? That's this directory. Made me chuckle. I kept it.

---

#### `RendezVoulns.Api`  

This is the entry point. A minimal .NET Web API. Bare bones by design. It does what it needs to, nothing more.

---

#### `RendezVoulns.Application`  

Application, domain, infrastructure. It’s all here, neatly separated like I’m trying to prove something. I might throw in a different architecture tomorrow. Or not. Depends how much coffee I’ve had.

Database? PostgreSQL in a Docker container. ORM? Dapper.

I’ve danced with EF Core before. It feels familiar, it’s smooth, maybe too smooth. I wanted something that bites back. I needed to feel every line of SQL like a slow bruise.

I chose Dapper because I want to *see* the queries. I wanted to stop letting tools hide the mess. No abstraction, no hand-holding. Just raw SQL and me.

With this kind of control comes responsibility and the privilege of screwing up gloriously in the name of learning.

As the project grows and queries get more complex, so do the chances for SQL injection to sneak in. This will be a place to explore these vulnerabilities from both sides of the table and learn how to harden against them. I’ll break things on purpose, trace the cracks, and patch them the right way.

---

#### `RendezVoulns.Contracts`  
This is the API's face. Request and response models. One goes in, the other comes out.