import React, { useEffect } from "react";
import { Button, Card, Modal } from "react-bootstrap";
import TaskForm from "./TaskForm";
import MessageBox from "./MessageBox";

const Task = ({ task, onEdit, onDelete }) => {
  const [action, setAction] = React.useState("");
  const dueDateTime = new Date(task.dueDate).toLocaleString([], {
    year: "numeric",
    month: "numeric",
    day: "numeric",

    hour12: false,
    hour: "2-digit",
    minute: "2-digit",
  });
  useEffect(() => {
    setAction("");
  }, [task]);

  return (
    <>
      <Card
        key={task.id}
        className="mb-3"
        onDoubleClick={() => setAction("view")}
      >
        <Card.Body>
          <Card.Title>{task.title}</Card.Title>
          <Card.Subtitle>{dueDateTime}</Card.Subtitle>
          <Card.Text>{task.comment}</Card.Text>
        </Card.Body>
        <Card.Footer>
          <Button
            variant="primary"
            size="sm"
            className="me-3"
            onClick={() => setAction("edit")}
          >
            Edit
          </Button>
          <Button
            variant="danger"
            size="sm"
            className="me-3"
            onClick={() => setAction("delete")}
          >
            Delete
          </Button>
        </Card.Footer>
      </Card>
      {action === "view" && (
        <TaskForm mode="View" task={task} onCancel={() => setAction("")} />
      )}
      {action === "edit" && (
        <TaskForm
          mode="Edit"
          task={task}
          onSubmit={onEdit}
          onCancel={() => setAction("")}
        />
      )}
      {action === "delete" && (
        <MessageBox
          title="Delete Task"
          message="Are you sure you want to delete this task?"
          DialogMode="YesNo"
          onYes={() => onDelete(task.id)}
          onClose={() => setAction("")}
        />
      )}
    </>
  );
};

export default Task;
