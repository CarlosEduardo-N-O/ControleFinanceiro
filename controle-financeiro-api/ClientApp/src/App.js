// src/App.jsx
import React, { useState } from "react";
import LoginPage from "./pages/LoginPage";

function App() {
    const [usuario, setUsuario] = useState(null);

    if (!usuario) {
        return <LoginPage onLogin={setUsuario} />;
    }

    return (
        <div>
            <h1>Bem-vindo, {usuario.nome}</h1>
            {/* aqui você renderiza a dashboard futura */}
        </div>
    );
}

export default App;
