#!/bin/bash

declare -A TASKS=(
    ["1"]="Language Practice"
    ["2"]="Morning Routine"
    ["3"]="Study C#"
    ["4"]="Team Meeting"
    ["5"]="Server Maintenance"
    ["6"]="Gym"
    ["7"]="Refactor Mapper"
    ["8"]="Buy Office Supplies"
    ["9"]="Review Documentation"
    ["10"]="Plan Sprint"
)

update_task() {
    local TASK_NAME=$1
    read -p "Enter the ID for '$TASK_NAME' (or press Enter to skip): " TASK_ID

    if [ -n "$TASK_ID" ]; then
        read -p "Enter new title for '$TASK_NAME': " NEW_TITLE
        read -p "Enter new description for '$TASK_NAME': " NEW_DESC

        RESPONSE=$(curl -s -k -X PUT "https://localhost:7002/api/tasks/update?id=$TASK_ID" \
            -H "Content-Type: application/json" \
            -d "{\"title\":\"$NEW_TITLE\",\"description\":\"$NEW_DESC\"}")

        echo "'$TASK_NAME' updated successfully. Server response:"
        echo "$RESPONSE"
    else
        echo "Skipped '$TASK_NAME'."
    fi
    echo
}

while true; do
    echo "Select a task to update (or 0 to exit):"
    for key in "${!TASKS[@]}"; do
        echo "$key) ${TASKS[$key]}"
    done

    read -p "Your choice: " CHOICE

    if [ "$CHOICE" == "0" ]; then
        echo "Exiting..."
        break
    elif [[ -n "${TASKS[$CHOICE]}" ]]; then
        update_task "${TASKS[$CHOICE]}"
    else
        echo "Invalid choice, try again."
    fi
done
