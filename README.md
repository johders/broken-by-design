# Broken By Design

Some things are made to work. This one's made to break, so I can learn how to fix it.

A multi-API .NET application designed to function as an event meetup platform while serving as a practical lab for exploring and mitigating security vulnerabilities.

The primary goal of this project is to deepen my understanding of modern, distributed .NET systems by building one from the ground up, with a special focus on exploring and mitigating common security vulnerabilities.

---

⚠️ Heads up: this project's under construction. Rough spots, broken parts, and a whole lot of lessons along the way. Strap in.

---

## Table of Contents

- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
  - [The RendezVoulns Directory — the main API](#the-rendezvoulns-directory--the-main-api)
  - [RendezVoulns.Api](#rendezvoulnsapi)
  - [RendezVoulns.Application](#rendezvoulnsapplication)
  - [RendezVoulns.Contracts](#rendezvoulnscontracts)
- [Validation & Error Handling](#validation--error-handling)
  - [RFC 7807 Problem Details for HTTP APIs](#rfc-7807-problem-details-for-http-apis)
  - [Presentation layer](#presentation-layer)
  - [Service layer](#service-layer)
  - [Repository layer](#repository-layer)

---

## Tech Stack
- .NET 9 Minimal Web API
- PostgreSQL (via Docker)
- Dapper ORM

---

## Project Structure  
### The *RendezVoulns* Directory — the main API

What if *"Rendezvous"* and *"Vulnerabilities"* had a weird-looking baby, and I decided to raise it? That's this directory. Made me chuckle. I kept it.

#### `RendezVoulns.Api`  

This is the entry point. A minimal .NET Web API. Bare bones by design. It does what it needs to, nothing more.

#### `RendezVoulns.Application`  

Application, domain, infrastructure. It’s all here, neatly separated like I’m trying to prove something. I might throw in a different architecture tomorrow. Or not. Depends how much coffee I’ve had.

Database? PostgreSQL in a Docker container. ORM? Dapper.

I’ve danced with EF Core before. It feels familiar, it’s smooth, maybe too smooth. I wanted something that bites back. I needed to feel every line of SQL like a slow bruise.

I chose Dapper because I want to *see* the queries. I wanted to stop letting tools hide the mess. No abstraction, no hand-holding. Just raw SQL and me.

With this kind of control comes responsibility and the privilege of screwing up gloriously in the name of learning.

As the project grows and queries get more complex, so do the chances for SQL injection to sneak in. This will be a place to explore these vulnerabilities from both sides of the table and learn how to harden against them. I’ll break things on purpose, trace the cracks, and patch them the right way.

#### `RendezVoulns.Contracts`  
This is the API's face. Request and response models. One goes in, the other comes out.

## Validation & Error Handling

Things will eventually go wrong, so I built the system to complain early, clearly, and in all the right places.

### RFC 7807 Problem Details for HTTP APIs

Errors aren’t just thrown around like loose screws. They’re carefully shaped into [RFC 7807](https://datatracker.ietf.org/doc/html/rfc7807)-style Problem Details before being returned from the API. Ready for humans and frontend devs to understand and act on.

The three layers of defence:

### Presentation layer

I believe in the goodness of humanity, just not in client input.
FluentValidation filters guard the endpoints, keeping the application clean from malformed requests.
Your request doesn't make sense? I’ll reply with a proper status code and a problem detail that tells you exactly what went wrong, without your input ever touching the rest of the application.

### Service layer

Even if a request survives initial validation, I still don’t trust it. Proactive checks like *"Does this title already exist in this group?"* live here.
The service layer returns `Result<T>` objects: either success with your data, or failure with a clear, structured reason why it didn’t work.

### Repository layer

The database gets the last word. Reactive checks catch rare edge cases like duplicate keys and foreign key violations. 
When they happen, custom exceptions are thrown, caught, and mapped to human-friendly errors.

All other unexpected exceptions are caught by global error handling and also turned into problem details.