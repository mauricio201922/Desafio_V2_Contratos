<template>
    <div class="container">
        <div class="custom-filter">
            <select v-model="arquivo" style="width: 150px;">
                <option style="color: black;" :value="null">Selecione...</option>
                <option style="color: black;" 
                    v-for="item in dataFiles" 
                    :value="item.path">
                    {{ item.path.split('\\').reverse()[0] }}
                </option>
            </select>
            <button @click="Buscar" class="custom-button">Buscar</button>

            <div class="custom-autenticacao-usuario">
                <input type="text" v-model="userName" placeholder="Nome do usuário..." />
                <button @click="autenticar" class="custom-button" :disabled="token">Autenticar usuário</button>
            </div>

            <div class="custom-file">
                <input type="file" @change="submitFile" accept=".csv" name="file" />
            </div>
        </div>

        <div class="custom-table">
            <table>
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>CPF</th>
                        <th>Contrato</th>
                        <th>Produto</th>
                        <th>Vencimento</th>
                        <th>Valor</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in post">
                        <td>{{ item.nome }}</td>
                        <td>{{ item.cpf }}</td>
                        <td>{{ item.contrato }}</td>
                        <td>{{ item.produto }}</td>
                        <td>{{ item.vencimento }}</td>
                        <td>{{ item.valor }}</td>
                    </tr>
                </tbody>
            </table>
            <table style="margin-top: 8px;">
                <thead>
                    <tr>
                        <th>Path</th>
                        <th>Usuário</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in dataFiles">
                        <td>{{ item.path }}</td>
                        <td>{{ item.usuario.name }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="js">

    export default {
        data() {
            return {
                loading: false,
                post: null,
                search: "",
                produto: null,
                userName: null,
                token: null,
                dataFiles: null,
                arquivo: null
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
            this.userName = localStorage.getItem("userName")
            this.token = localStorage.getItem("token")
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'

        },
        computed: {
            menorDataFunc() {
                
            }
        },
        methods: {
            fetchData() {
                this.post = null;

                fetch('https://localhost:7252/api/Contratos/GetAllContratos', { headers: { Authorization: `Bearer ${localStorage.getItem("token")}` } })
                    .then(r => r.json())
                    .then(json => {
                        fetch('https://localhost:7252/api/Contratos/GetFileContratos', { headers: { Authorization: `Bearer ${localStorage.getItem("token")}` } })
                            .then(r => r.json())
                            .then(json => {
                                this.dataFiles = json;
                                return;
                            });
                        this.post = json;
                        return;
                    });
            },
            autenticar() {
                if(!this.userName) return;
                if(localStorage.getItem("token")) return;
                fetch(`https://localhost:7252/api/autenticacao/${this.userName}`, {method: 'POST'})
                    .then(response => response.text())
                    .then(r => {
                        localStorage.setItem("userName", this.userName)
                        localStorage.setItem("token", r)
                        this.token = r
                        return;
                    });
                
            },
            async submitFile(e) {
                var formdata = new FormData();
                formdata.append("file", e.target.files[0]);
                fetch(`https://localhost:7252/api/Contratos/PostFile/${localStorage.getItem("userName")}`, {body: formdata, method: 'POST', headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }})
                    .then(response => response.json())
                    .then(r => {
                        this.post = r;
                        return;
                    });
            },
            Buscar() {
                if (this.arquivo != null) {
                    var formdata = new FormData();
                    formdata.append("path", this.arquivo.toString());
                    console.log(this.arquivo.toString())
                    fetch(`https://localhost:7252/api/Contratos/GetFileContratos`, { body: formdata, headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }, method: 'POST' })
                            .then(r => r.json())
                            .then(json => {
                                this.dataFiles = json;
                                this.arquivo == null
                                return;
                            });
                }
                else {
                    this.fetchData()
                }
            }
        },
    };
</script>

<style scoped>
.container {
    margin: 10px;
    display: flex;
    flex-wrap: nowrap;
    flex-direction: column;
}
.custom-filter {
    display: flex;
    justify-content: space-between;
}
.custom-filter input, select {
    color: white;
    background: none;
    border: 0;
    border-bottom: 1px solid;
    border-radius: 5%;
    border-color: black;
    transition: 0.6s ease-in-out;
}
.custom-filter input:focus {
    outline: 0;
    border-bottom-color: white;
    transition: 0.6s ease-in-out;
}
.custom-button {
    background-color: aqua;
    border: 1px solid black;
    height: 30px;
    transition: 0.7s;
}

.custom-button:hover {
    background-color: aqua;
    border: 2px solid aquamarine;
    opacity: 0.75;
    cursor: pointer;
    transform: scale(1.1, 1.1);
    transition: 0.7s;
}
.custom-button:disabled {
    opacity: 0.5;
    outline: 0;
    border: 0;
    cursor: auto;
    transform: scale(1,1);
}

.custom-filter input, select, button {
    margin-left: 3px;
    margin-right: 3px;
}

.custom-table {
    margin-top: 25px;
}

.custom-table table {
    border-collapse: collapse;
}

.custom-table table td, th {
    border: 1px solid #999;
    padding: 0.5rem;
    text-align: left;
}

.custom-table table tbody tr:nth-child(odd) {
  background: #5c5c5c;
}

.custom-autenticacao-usuario {
    margin-left: 35px;
}

.custom-autenticacao-usuario input {
    width: 35%;
}
</style>