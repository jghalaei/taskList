import { useState, useEffect } from "react";
import NavHeader from "./Components/NavHeader";
import TaskList from "./Components/TaskList";
import LoginForm from "./Components/LoginForm";
import { Container } from "react-bootstrap";
import MessageBox from "./Components/MessageBox";
import TaskForm from "./Components/TaskForm";
import WeekShow from "./Components/WeekShow";
function App() {
  const [tasks, setTasks] = useState([]);
  const [user, setUser] = useState({});
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [action, setAction] = useState("");
  const [refresh, setRefresh] = useState(false);
  const [showDate, setShowDate] = useState(new Date());
  const backendUrl = process.env.REACT_APP_BACKEND_URL;

  useEffect(() => {
    if (user?.jwtToken) {
      console.log("user already have token");
      localStorage.setItem("user", JSON.stringify(user));
      setIsLoggedIn(true);
      loadTasks();
      setShowDate(new Date());
    } else {
      console.log("user getting from local storage");
      const user = JSON.parse(localStorage.getItem("user"));
      if (user?.jwtToken) {
        setUser(user);
      }
    }
  }, [user, refresh]);
  const handleLogin = (userTest) => {
    setUser(userTest);
    //setIsLoggedIn(true);
    setAction("");
  };

  const loadTasks = async () => {
    try {
      const res = await fetch(`${backendUrl}/tasks`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${user.jwtToken}`,
        },
      });
      if (res.status === 200) {
        const tasks = await res.json();
        setTasks(tasks);
      } else {
        const error = await res.text();
        alert(error);
      }
    } catch (error) {
      alert(error);
    }
  };
  const handleLogout = () => {
    setUser({
      userId: "",
      userName: "",
      jwtToken: "",
    });
    localStorage.removeItem("user");
    setIsLoggedIn(false);
    setTasks([]);
    setAction("");
  };
  const handleAddTask = async (task) => {
    try {
      const res = await fetch(`${backendUrl}/tasks`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${user.jwtToken}`,
        },
        body: JSON.stringify(task),
      });
      if (!res.ok) {
        const error = await res.text();
        console.log(error);
        return;
      }
      const newTask = await res.json();
      setTasks([...tasks, newTask]);
      setAction("");
    } catch (error) {
      console.log(error);
      alert(error);
    }
  };
  const handleEditTask = async (task) => {
    try {
      const res = await fetch(`${backendUrl}/tasks/update`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${user.jwtToken}`,
        },
        body: JSON.stringify(task),
      });
      if (!res.ok) {
        const error = await res.text();
        console.log(error);
        return;
      }
      const newTasks = tasks.map((t) => (t.id === task.id ? task : t));
      setTasks(newTasks);
      setAction("");
    } catch (error) {
      console.log(error);
      alert(error);
    }
  };
  const handleDeleteTask = async (taskId) => {
    try {
      const res = await fetch(`${backendUrl}/tasks/${taskId}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${user.jwtToken}`,
        },
      });
      if (!res.ok) {
        const error = await res.text();
        console.log(error);
        return;
      }
      const newTasks = tasks.filter((task) => task.id !== taskId);
      setTasks(newTasks);
      setAction("");
    } catch (error) {
      console.log(error);
      alert(error);
    }
  };
  return (
    <div className="App">
      <NavHeader
        isLoggedIn={isLoggedIn}
        user={user}
        onLoginClicked={() => setAction("login")}
        onSignupClicked={() => setAction("signup")}
        onLogoutClicked={() => setAction("logout")}
        onAddTaskClicked={() => setAction("addTask")}
        onRefreshClicked={() => setRefresh(!refresh)}
      />

      <Container>
        {action === "login" && <LoginForm onLogin={handleLogin} />}
        {action === "logout" && (
          <MessageBox
            title="Logout"
            message="Are you sure you want to logout?"
            DialogMode="YesNo"
            onYes={handleLogout}
            onClose={() => setAction("")}
          />
        )}
        {action === "addTask" && (
          <TaskForm onCancel={() => setAction("")} onSubmit={handleAddTask} />
        )}
        {action === "" && isLoggedIn && (
          <WeekShow
            showDate={showDate}
            tasks={tasks}
            onEdit={handleEditTask}
            onDelete={handleDeleteTask}
            setShowDate={setShowDate}
          />
        )}
      </Container>
    </div>
  );
}

export default App;
