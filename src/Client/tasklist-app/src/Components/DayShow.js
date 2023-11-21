import React from "react";
import { Stack } from "react-bootstrap";
import Task from "./Task";
const DayShow = (props) => {
  const { tasks, onEdit, onDelete } = props;
  const hours = [...Array(24)].map((_, index) => {
    return index;
  });
  return (
    <Stack gap={3} direction="vertical" className="w-100 p-3 border">
      {hours.map((hour) => (
        <div key={hour} className="d-flex justify-content-center">
          {tasks
            .filter((task) => new Date(task.dueDate).getHours() === hour)
            .map((task) => (
              <Task
                key={task.id}
                task={task}
                onEdit={onEdit}
                onDelete={onDelete}
              />
            ))}
        </div>
      ))}
    </Stack>
  );
};
export default DayShow;
