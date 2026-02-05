# LynnBlog Engine

AI-Native Blog Engine built with .NET 8 + Vue 3 + Nuxt 3.

## Architecture

```
LynnBlog/
├── apps/
│   ├── api/           # .NET 8 Web API
│   ├── admin/         # Vue 3 + Vite (SPA)
│   └── web/           # Nuxt 3 (SSR)
├── packages/
│   └── shared/        # Shared packages
├── docker/
│   └── docker-compose.yml
└── scripts/
    └── migrate-hugo.sh
```

## Tech Stack

- **Backend**: .NET 8, ASP.NET Core, EF Core, PostgreSQL + pgvector
- **Frontend Admin**: Vue 3, Vite, TailwindCSS
- **Frontend Web**: Nuxt 3, Vue 3, TailwindCSS
- **AI Integration**: OpenAI/Claude API, pgvector
- **Deployment**: Docker, GitHub Actions

## Quick Start

```bash
# Clone the repo
git clone https://github.com/OrangeRed0706/lynnblog.git
cd lynnblog

# Start with Docker Compose
docker-compose up -d

# Or develop locally
cd apps/api && dotnet run
cd apps/admin && npm install && npm run dev
cd apps/web && npm install && npm run dev
```

## License

MIT
