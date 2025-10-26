#!/bin/bash

curl -k -X POST https://localhost:7002/api/tasks/add \
-H "Content-Type: application/json" \
-d '{"title":"Language Practice","description":"Study English vocabulary and grammar for 30 minutes"}'

curl -k -X POST https://localhost:7002/api/tasks/add \
-H "Content-Type: application/json" \
-d '{"title":"Morning Routine","description":"Wake up, make bed, brush teeth, and have breakfast"}'

curl -k -X POST https://localhost:7002/api/tasks/add \
-H "Content-Type: application/json" \
-d '{"title":"Study C#","description":"Review records, DTOs, and ASP.NET Core Controllers"}'

curl -k -X POST https://localhost:7002/api/tasks/add \
-H "Content-Type: application/json" \
-d '{"title":"Team Meeting","description":"Discuss backlog and priorities for the sprint"}'

curl -k -X POST https://localhost:7002/api/tasks/add \
-H "Content-Type: application/json" \
-d '{"title":"Server Maintenance","description":"Install security patch and restart services"}'

curl -k -X POST https://localhost:7002/api/tasks/add \
-H "Content-Type: application/json" \
-d '{"title":"Gym","description":"Leg workout and cardio at 6 PM"}'

curl -k -X POST https://localhost:7002/api/tasks/add \
-H "Content-Type: application/json" \
-d '{"title":"Refactor Mapper","description":"Improve TaskPresentationMapper to support new fields"}'

curl -k -X POST https://localhost:7002/api/tasks/add \
-H "Content-Type: application/json" \
-d '{"title":"Buy Office Supplies","description":"Buy pens, paper, and sticky notes"}'

curl -k -X POST https://localhost:7002/api/tasks/add \
-H "Content-Type: application/json" \
-d '{"title":"Review Documentation","description":"Update README and internal guides"}'

curl -k -X POST https://localhost:7002/api/tasks/add \
-H "Content-Type: application/json" \
-d '{"title":"Plan Sprint","description":"Define tasks and estimates for the next sprint"}'