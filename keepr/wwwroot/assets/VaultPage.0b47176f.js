import{u as g,k,C as y,c,a as p,o as s,b as u,d as t,j as V,t as d,m as x,F as v,l as K,e as w,D as B}from"./vendor.9cbc420e.js";import{_ as C,v as _,l as i,P as r,A as m}from"./index.d2f3ff0e.js";const D={setup(){const l=g(),o=k();return document.title="Keepr | Vault",y(async()=>{try{await _.getById(o.params.id),i.log("push vault")}catch(a){l.push({name:"Home"}),i.error(a),r.toast(a.message,"error")}}),{vault:c(()=>m.activeVault),keeps:c(()=>m.keeps),account:c(()=>m.account),async deleteVault(a){try{if(await r.confirm("Do you want to delete your vault?"))await _.deleteVault(a),l.push({name:"Account"}),r.toast("Deleted vault","success");else return}catch(e){i.error(e),r.toast(e.message,"error")}}}}},N={class:"vault-page"},b={class:"container"},j={class:"col-12 m-3"},A={class:"masonry-with-columns"};function M(l,o,a,e,P,S){const h=p("Keep"),f=p("KeepModal");return s(),u(v,null,[t("div",N,[t("div",b,[t("div",j,[t("h1",null,[V(d(e.vault.name)+" ",1),e.vault.creatorId===e.account.id?(s(),u("i",{key:0,class:"mdi mdi-delete-outline selectable rounded text-danger",onClick:o[0]||(o[0]=n=>e.deleteVault(e.vault.id)),title:"delete vault"})):x("",!0)]),t("h4",null,d(e.vault.description),1),t("h4",null,"Keeps: "+d(e.keeps.length),1)]),t("div",A,[(s(!0),u(v,null,K(e.keeps,n=>(s(),B(h,{keep:n,key:n.id},null,8,["keep"]))),128))])])]),w(f)],64)}var R=C(D,[["render",M]]);export{R as default};